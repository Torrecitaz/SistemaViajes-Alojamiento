using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;
using Alojamiento.Business.Utils;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Entities;
using Alojamiento.Domain.Entities.Marketing;

namespace Alojamiento.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AlojamientoDbContext _context;

        public UsuarioService(AlojamientoDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDTO> RegistrarUsuarioAsync(UsuarioCreateDTO usuarioDto)
        {
            // Regla de Negocio: Validar duplicidad de email (TC-F02)
            bool existe = await _context.Usuarios.AnyAsync(u => u.Email == usuarioDto.Correo);
            if (existe)
            {
                throw new InvalidOperationException("El correo ya está registrado."); // Controller devolverá 409 Conflict
            }

            var usuario = new Usuario
            {
                NombreCompleto = usuarioDto.Nombre,
                Email = usuarioDto.Correo,
                Telefono = usuarioDto.Telefono,
                FotoUrl = usuarioDto.FotoPerfilUrl
                // Domicilio y EsColaborador se manejan en Cliente y roles
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return MapToDTO(usuario);
        }

        public async Task<UsuarioDTO?> ObtenerPorIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario == null ? null : MapToDTO(usuario);
        }

        public async Task<PagedList<UsuarioDTO>> ListarUsuariosAsync(int pageNumber, int pageSize, string? nombre)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(u => u.NombreCompleto.Contains(nombre));
            }

            var dtoQuery = query.Select(u => new UsuarioDTO
            {
                Id = u.UsuarioId,
                Nombre = u.NombreCompleto,
                Correo = u.Email,
                Telefono = u.Telefono,
                FotoPerfilUrl = u.FotoUrl,
                Domicilio = "", // TODO: Map from Cliente
                Calificacion = 5.0,
                EsColaborador = false // TODO: Map from Roles
            });

            return await PagedList<UsuarioDTO>.CreateAsync(dtoQuery, pageNumber, pageSize);
        }

        public async Task<UsuarioDTO> ActualizarUsuarioAsync(int id, UsuarioCreateDTO usuarioDto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) throw new Exception("Usuario no encontrado.");

            // Revisar si cambia el correo y si ya existe
            if (usuario.Email != usuarioDto.Correo)
            {
                bool existe = await _context.Usuarios.AnyAsync(u => u.Email == usuarioDto.Correo);
                if (existe) throw new InvalidOperationException("El correo ya está registrado.");
            }

            usuario.NombreCompleto = usuarioDto.Nombre;
            usuario.Email = usuarioDto.Correo;
            usuario.Telefono = usuarioDto.Telefono;
            usuario.FotoUrl = usuarioDto.FotoPerfilUrl;

            await _context.SaveChangesAsync();
            return MapToDTO(usuario);
        }

        public async Task<UsuarioDTO> ActualizarParcialAsync(int id, string telefono)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) throw new Exception("Usuario no encontrado.");

            usuario.Telefono = telefono;
            await _context.SaveChangesAsync();
            return MapToDTO(usuario);
        }

        public async Task EliminarUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SumarPuntosAsync(int usuarioId, int puntos)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
            if (cliente == null) throw new Exception("Cliente no encontrado para sumar puntos.");

            var puntosC = await _context.PuntosClientes.FirstOrDefaultAsync(p => p.ClienteId == cliente.ClienteId);
            if (puntosC == null)
            {
                puntosC = new PuntosCliente { ClienteId = cliente.ClienteId, PuntosAcumulados = 0 };
                _context.PuntosClientes.Add(puntosC);
                await _context.SaveChangesAsync(); // Para obtener el ID
            }

            puntosC.PuntosAcumulados += puntos;

            _context.HistorialPuntosClientes.Add(new HistorialPuntosCliente
            {
                PuntosClienteId = puntosC.PuntosClienteId,
                Cantidad = puntos,
                TipoTransaccion = "Acumulacion",
                Descripcion = "Puntos sumados por actividad reciente"
            });

            await _context.SaveChangesAsync();
        }

        private static UsuarioDTO MapToDTO(Usuario u) => new UsuarioDTO
        {
            Id = u.UsuarioId,
            Nombre = u.NombreCompleto,
            Correo = u.Email,
            Telefono = u.Telefono,
            FotoPerfilUrl = u.FotoUrl,
            Domicilio = "", // TODO: Map from Cliente
            Calificacion = 5.0,
            EsColaborador = false // TODO: Map from Roles
        };
    }
}