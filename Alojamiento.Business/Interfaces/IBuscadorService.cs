using Alojamiento.Domain.Entities;
using Alojamiento.Business.DTOs;

namespace Alojamiento.Business.Interfaces
{
    public interface IBuscadorService
    {
        Task<IEnumerable<PropiedadDto>> BuscarPropiedadesAsync(string? ciudad, bool? admiteMascotas, bool? tienePiscina, bool? tieneWifi, decimal? precioMaximo, string? monedaDestino = null);
    }
}
