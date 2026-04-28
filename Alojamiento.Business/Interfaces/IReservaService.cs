using Alojamiento.Domain.Entities;

namespace Alojamiento.Business.Interfaces
{
    public interface IReservaService
    {
        // Verifica si la propiedad está libre en el periodo seleccionado 
        Task<bool> VerificarDisponibilidadAsync(int propiedadId, DateTime inicio, DateTime fin);
        
        // Calcula el costo total basado en las noches de estadía 
        decimal CalcularCostoTotal(decimal tarifaPorNoche, DateTime inicio, DateTime fin);
        
        // Registra la reserva en la base de datos 
        Task<Reserva> RegistrarReservaAsync(Reserva reserva);
    }
}