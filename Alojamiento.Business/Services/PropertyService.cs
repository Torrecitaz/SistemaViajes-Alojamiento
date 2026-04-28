using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;
using Alojamiento.Business.Utils;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Enums;
using Alojamiento.Domain.Entities.Alojamientos;
using System.IO;
using System;

namespace Alojamiento.Business.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AlojamientoDbContext _context;

        public PropertyService(AlojamientoDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<PropertyResponseDTO>> GetPropertiesAsync(int pageNumber, int pageSize, Alojamiento.Domain.Enums.TipoAlojamiento? tipo, decimal? maxPrice, bool? admitenMascotas)
        {
            var query = _context.Propiedades.Include(p => p.Servicios).AsQueryable();

            if (tipo.HasValue)
            {
                // TODO: Fix this query when the enum vs entity logic is fully migrated
                // query = query.Where(p => p.TipoAlojamientoId == (int)tipo.Value);
            }

            if (maxPrice.HasValue)
            {
                // query = query.Where(p => p.PrecioPorNoche <= maxPrice.Value);
            }

            if (admitenMascotas.HasValue && admitenMascotas.Value)
            {
                query = query.Where(p => p.AdmiteMascotas);
            }

            var dtoQuery = query.Select(p => new PropertyResponseDTO
            {
                Id = p.PropiedadId,
                Nombre = p.Nombre,
                Direccion = p.Direccion,
                TipoAlojamiento = Alojamiento.Domain.Enums.TipoAlojamiento.Hotel, // Placeholder
                PrecioBase = 0m,
                Moneda = "USD",
                Calificacion = (double)p.CalificacionPromedio,
                Instalaciones = p.Servicios.Where(s => s.Servicio != null).Select(s => s.Servicio!.Nombre).ToList()
            });

            return await PagedList<PropertyResponseDTO>.CreateAsync(dtoQuery, pageNumber, pageSize);
        }

        public async Task<PropertyResponseDTO> GetPropertyByIdAsync(int id)
        {
            var property = await _context.Propiedades
                .Include(p => p.Servicios)
                .ThenInclude(ps => ps.Servicio)
                .FirstOrDefaultAsync(p => p.PropiedadId == id);

            if (property == null) return null;

            return new PropertyResponseDTO
            {
                Id = property.PropiedadId,
                Nombre = property.Nombre,
                Direccion = property.Direccion,
                TipoAlojamiento = Alojamiento.Domain.Enums.TipoAlojamiento.Hotel, // Placeholder
                PrecioBase = 0m,
                Moneda = "USD",
                Calificacion = (double)property.CalificacionPromedio,
                Instalaciones = property.Servicios.Where(ps => ps.Servicio != null).Select(ps => ps.Servicio!.Nombre).ToList(),
                Fotos = property.Fotos.OrderBy(f => f.Orden).Select(f => f.Url).ToList()
            };
        }

        public async Task<PropertyResponseDTO> CreatePropertyAsync(PropertyCreateDTO dto, int usuarioId, string webRootPath)
        {
            // 1. Obtener Anfitrión
            var anfitrion = await _context.Anfitriones.FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);
            if (anfitrion == null) throw new InvalidOperationException("El usuario no es un anfitrión válido.");

            // 2. Crear Entidad Propiedad
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

            // 5. Guardar Fotos Físicamente
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

            return await GetPropertyByIdAsync(propiedad.PropiedadId);
        }
    }
}
