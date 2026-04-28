using Alojamiento.Business.Interfaces;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alojamiento.Business.Services
{
    public class ReservaService : IReservaService
    {
        private readonly AlojamientoDbContext _context;

        public ReservaService(AlojamientoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> VerificarDisponibilidadAsync(int propiedadId, DateTime inicio, DateTime fin)
        {
            // Lógica para detectar traslapes de fechas en el periodo de llegada y salida 
            var existeTraslape = await _context.Reservas
                .AnyAsync(r => r.PropiedadId == propiedadId && 
                          ((inicio >= r.FechaCheckIn && inicio < r.FechaCheckOut) || 
                           (fin > r.FechaCheckIn && fin <= r.FechaCheckOut) ||
                           (inicio <= r.FechaCheckIn && fin >= r.FechaCheckOut)));

            return !existeTraslape;
        }

        public decimal CalcularCostoTotal(decimal tarifaPorNoche, DateTime inicio, DateTime fin)
        {
            // Calcula la diferencia de días para mostrar el costo total al cliente 
            var noches = (fin - inicio).Days;
            if (noches <= 0) return 0;
            
            return noches * tarifaPorNoche;
        }

        public async Task<Reserva> RegistrarReservaAsync(Reserva reserva)
        {
            // Validación de seguridad: el Check-out debe ser después del Check-in 
            if (reserva.FechaCheckOut <= reserva.FechaCheckIn)
                throw new Exception("La fecha de salida debe ser posterior a la de entrada.");

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return reserva;
        }
    }
}