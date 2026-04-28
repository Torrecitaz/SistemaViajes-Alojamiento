using Alojamiento.Domain.Entities;

namespace Alojamiento.Business.Interfaces
{
    public interface IAlojamientoService
    {
        // CRUD de Propiedades
        Task<IEnumerable<Propiedad>> ObtenerPropiedadesAsync();
        Task<Propiedad> CrearPropiedadAsync(Propiedad propiedad);
        
        // Lógica de Reservas
        Task<Reserva> ProcesarReservaAsync(Reserva reserva);
    }
}