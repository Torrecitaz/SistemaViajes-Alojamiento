using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;
using Alojamiento.Business.Utils;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Entities.Alojamientos;
using Alojamiento.Domain.Entities.Seguridad;

namespace Alojamiento.Business.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AlojamientoDbContext _context;

        public PropertyService(AlojamientoDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<PropertyResponseDTO>> GetPropertiesAsync(
            int pageNumber, int pageSize,
            Alojamiento.Domain.Enums.TipoAlojamiento? tipo, decimal? maxPrice, bool? admitenMascotas)
        {
            var query = _context.Propiedades
                .Include(p => p.Servicios).ThenInclude(ps => ps.Servicio)
                .Include(p => p.Fotos)
                .Include(p => p.TipoAlojamiento)
                .Include(p => p.Habitaciones).ThenInclude(h => h.Tarifas)
                .Include(p => p.Ciudad).ThenInclude(c => c!.Pais)
                .Where(p => !p.EliminadoLogico)
                .AsQueryable();

            if (admitenMascotas.HasValue && admitenMascotas.Value)
            {
                query = query.Where(p => p.AdmiteMascotas);
            }

            var dtoQuery = query.Select(p => new PropertyResponseDTO
            {
                Id = p.PropiedadId,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion ?? "",
                Direccion = p.Direccion,
                Ciudad = p.Ciudad != null ? p.Ciudad.Nombre : "",
                Pais = p.Ciudad != null && p.Ciudad.Pais != null ? p.Ciudad.Pais.Nombre : "",
                TipoAlojamientoNombre = p.TipoAlojamiento != null ? p.TipoAlojamiento.Nombre : "",
                PrecioBase = p.Habitaciones
                    .SelectMany(h => h.Tarifas)
                    .OrderBy(t => t.PrecioPorNoche)
                    .Select(t => (decimal?)t.PrecioPorNoche)
                    .FirstOrDefault() ?? 0m,
                Moneda = "USD",
                Calificacion = (double)p.CalificacionPromedio,
                TotalResenas = p.TotalResenas,
                EstadoPropiedad = p.EstadoPropiedad,
                AdmiteMascotas = p.AdmiteMascotas,
                CapacidadMaxima = p.Habitaciones.Sum(h => h.CapacidadAdultos),
                Instalaciones = p.Servicios
                    .Where(s => s.Servicio != null)
                    .Select(s => s.Servicio!.Nombre).ToList(),
                Fotos = p.Fotos
                    .Where(f => !f.EliminadoLogico)
                    .OrderBy(f => f.Orden)
                    .Select(f => f.Url).ToList()
            });

            return await PagedList<PropertyResponseDTO>.CreateAsync(dtoQuery, pageNumber, pageSize);
        }

        public async Task<PropertyResponseDTO?> GetPropertyByIdAsync(int id)
        {
            var p = await _context.Propiedades
                .Include(x => x.Servicios).ThenInclude(ps => ps.Servicio)
                .Include(x => x.Fotos)
                .Include(x => x.TipoAlojamiento)
                .Include(x => x.Habitaciones).ThenInclude(h => h.Tarifas)
                .Include(x => x.Ciudad).ThenInclude(c => c!.Pais)
                .Include(x => x.Politica)
                .FirstOrDefaultAsync(x => x.PropiedadId == id && !x.EliminadoLogico);

            if (p == null) return null;

            return new PropertyResponseDTO
            {
                Id = p.PropiedadId,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion ?? "",
                Direccion = p.Direccion,
                Ciudad = p.Ciudad?.Nombre ?? "",
                Pais = p.Ciudad?.Pais?.Nombre ?? "",
                TipoAlojamientoNombre = p.TipoAlojamiento?.Nombre ?? "",
                PrecioBase = p.Habitaciones
                    .SelectMany(h => h.Tarifas)
                    .OrderBy(t => t.PrecioPorNoche)
                    .Select(t => (decimal?)t.PrecioPorNoche)
                    .FirstOrDefault() ?? 0m,
                Moneda = "USD",
                Calificacion = (double)p.CalificacionPromedio,
                TotalResenas = p.TotalResenas,
                EstadoPropiedad = p.EstadoPropiedad,
                AdmiteMascotas = p.AdmiteMascotas,
                CapacidadMaxima = p.Habitaciones.Sum(h => h.CapacidadAdultos),
                Instalaciones = p.Servicios
                    .Where(s => s.Servicio != null)
                    .Select(s => s.Servicio!.Nombre).ToList(),
                Fotos = p.Fotos
                    .Where(f => !f.EliminadoLogico)
                    .OrderBy(f => f.Orden)
                    .Select(f => f.Url).ToList()
            };
        }

        public async Task<PropertyResponseDTO> CreatePropertyAsync(PropertyCreateDTO dto, int usuarioId, string webRootPath)
        {
            // 1. Obtener Anfitrión — si es Admin y no tiene perfil Anfitrion, crear uno auto
            var anfitrion = await _context.Anfitriones.FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);
            if (anfitrion == null)
            {
                // Verificar que el usuario existe
                var usuario = await _context.Usuarios.FindAsync(usuarioId);
                if (usuario == null) throw new InvalidOperationException("Usuario no encontrado.");

                // Auto-crear perfil anfitrión para admins que publican
                anfitrion = new Anfitrion
                {
                    UsuarioId = usuarioId,
                    Verificado = true,
                    NombreEmpresa = usuario.NombreCompleto
                };
                _context.Anfitriones.Add(anfitrion);
                await _context.SaveChangesAsync();
            }

            // 2. Crear Propiedad
            var propiedad = new Propiedad
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Direccion = dto.Direccion,
                CiudadId = dto.CiudadId,
                TipoAlojamientoId = dto.TipoAlojamientoId,
                AnfitrionId = anfitrion.AnfitrionId,
                AdmiteMascotas = false,
                EstadoPropiedad = "Activa"
            };

            _context.Propiedades.Add(propiedad);
            await _context.SaveChangesAsync();

            // 3. Crear Habitación Base
            var habitacion = new Habitacion
            {
                PropiedadId = propiedad.PropiedadId,
                Nombre = "Unidad Principal",
                CapacidadAdultos = dto.CapacidadAdultos,
                NumBanos = 1,
                NumDormitorios = 1,
                AdmiteMascotas = false
            };
            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();

            // 4. Crear Tarifa de Habitación
            var moneda = await _context.Monedas.FirstOrDefaultAsync(m => m.Codigo == "USD");
            var tarifa = new TarifaHabitacion
            {
                HabitacionId = habitacion.HabitacionId,
                MonedaId = moneda?.MonedaId ?? 1,
                PrecioPorNoche = dto.PrecioPorNoche,
                FechaInicio = DateTime.UtcNow.Date,
                FechaFin = DateTime.UtcNow.Date.AddYears(1)
            };
            _context.TarifasHabitaciones.Add(tarifa);

            // 5. Guardar Fotos
            if (dto.Fotos != null && dto.Fotos.Count > 0)
            {
                var uploadsFolder = Path.Combine(webRootPath, "uploads", "properties", propiedad.PropiedadId.ToString());
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                int orden = 1;
                foreach (var foto in dto.Fotos)
                {
                    if (foto.Length > 0)
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await foto.CopyToAsync(fileStream);
                        }

                        var propiedadFoto = new PropiedadFoto
                        {
                            PropiedadId = propiedad.PropiedadId,
                            Url = $"/uploads/properties/{propiedad.PropiedadId}/{uniqueFileName}",
                            EsPrincipal = orden == 1,
                            Orden = orden
                        };
                        _context.PropiedadesFotos.Add(propiedadFoto);
                        orden++;
                    }
                }
            }

            await _context.SaveChangesAsync();

            return (await GetPropertyByIdAsync(propiedad.PropiedadId))!;
        }
    }
}
