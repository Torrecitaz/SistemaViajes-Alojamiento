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

        public async Task<IEnumerable<PropiedadDto>> BuscarPropiedadesAsync(
            string? ciudad, bool? admiteMascotas, bool? tienePiscina,
            bool? tieneWifi, decimal? precioMaximo, string? monedaDestino = null)
        {
            var query = _context.Propiedades
                .Include(p => p.Ciudad).ThenInclude(c => c!.Pais)
                .Include(p => p.TipoAlojamiento)
                .Include(p => p.Servicios).ThenInclude(s => s.Servicio)
                .Include(p => p.Habitaciones).ThenInclude(h => h.Tarifas)
                .Include(p => p.Fotos)
                .Where(p => !p.EliminadoLogico)
                .AsQueryable();

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
                query = query.Where(p => p.Servicios.Any(s => s.Servicio != null && s.Servicio.Nombre.Contains("WiFi")));
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
                Descripcion = p.Descripcion ?? "",
                Direccion = p.Direccion,
                Ciudad = p.Ciudad?.Nombre ?? "",
                Pais = p.Ciudad?.Pais?.Nombre ?? "",
                TipoAlojamientoNombre = p.TipoAlojamiento?.Nombre ?? "",
                PrecioBase = p.Habitaciones.SelectMany(h => h.Tarifas).Min(t => (decimal?)t.PrecioPorNoche) ?? 0,
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
            }).ToList();

            if (!string.IsNullOrEmpty(monedaDestino) && monedaDestino.ToUpper() != "USD")
            {
                foreach (var dto in resultados)
                {
                    dto.PrecioBase = await _currencyService.ConvertirMonedaAsync(dto.PrecioBase, "USD", monedaDestino);
                    dto.Moneda = monedaDestino.ToUpper();
                }
            }

            return resultados;
        }
    }
}
