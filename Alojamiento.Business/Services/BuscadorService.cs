using Alojamiento.Business.DTOs;
using Alojamiento.Business.Interfaces;
using Alojamiento.DataManagement.Context;
using Microsoft.EntityFrameworkCore;

namespace Alojamiento.Business.Services
{
    public class BuscadorService : IBuscadorService
    {
        private readonly AlojamientoDbContext _context;
        private readonly ICurrencyService _currencyService;

        public BuscadorService(AlojamientoDbContext context, ICurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        public async Task<IEnumerable<PropiedadDto>> BuscarPropiedadesAsync(string? ciudad, bool? admiteMascotas, bool? tienePiscina, bool? tieneWifi, decimal? precioMaximo, string? monedaDestino = null)
        {
            var query = _context.Propiedades.AsQueryable();

            if (!string.IsNullOrEmpty(ciudad))
            {
                query = query.Where(p => p.Ciudad != null && p.Ciudad.Nombre.Contains(ciudad));
            }

            if (admiteMascotas.HasValue && admiteMascotas.Value)
            {
                query = query.Where(p => p.AdmiteMascotas);
            }

            if (tienePiscina.HasValue && tienePiscina.Value)
            {
                query = query.Where(p => p.Servicios.Any(s => s.Servicio != null && s.Servicio.Nombre.Contains("Piscina")));
            }

            if (tieneWifi.HasValue && tieneWifi.Value)
            {
                query = query.Where(p => p.Servicios.Any(s => s.Servicio != null && s.Servicio.Nombre.Contains("Wifi")));
            }

            if (precioMaximo.HasValue)
            {
                query = query.Where(p => p.Habitaciones.Any(h => h.Tarifas.Any(t => t.PrecioPorNoche <= precioMaximo.Value)));
            }

            var propiedades = await query.ToListAsync();

            var resultados = propiedades.Select(p => new PropiedadDto
            {
                Id = p.PropiedadId,
                Nombre = p.Nombre,
                Ciudad = p.Ciudad?.Nombre ?? "",
                TipoAlojamiento = p.TipoAlojamiento?.Nombre ?? "",
                PrecioPorNoche = p.Habitaciones.SelectMany(h => h.Tarifas).Min(t => (decimal?)t.PrecioPorNoche) ?? 0,
                Moneda = "USD", // Por defecto asumimos USD para la base
                TieneWifi = p.Servicios.Any(s => s.Servicio != null && s.Servicio.Nombre.Contains("Wifi")),
                AdmiteMascotas = p.AdmiteMascotas,
                TienePiscina = p.Servicios.Any(s => s.Servicio != null && s.Servicio.Nombre.Contains("Piscina")),
                Calificacion = (double)p.CalificacionPromedio
            }).ToList();

            if (!string.IsNullOrEmpty(monedaDestino) && monedaDestino.ToUpper() != "USD")
            {
                foreach (var dto in resultados)
                {
                    dto.PrecioPorNoche = await _currencyService.ConvertirMonedaAsync(dto.PrecioPorNoche, "USD", monedaDestino);
                    dto.Moneda = monedaDestino.ToUpper();
                }
            }

            return resultados;
        }
    }
}
