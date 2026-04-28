using System;
using System.Threading.Tasks;
using Alojamiento.Business.DTOs;
using Alojamiento.Domain.Entities.Reservas;

namespace Alojamiento.Business.Interfaces
{
    public interface IBookingService
    {
        Task<Reserva> CreateBookingAsync(BookingCreateDTO bookingDto);
        Task<Reserva> CreateBookingAsync(BookingCreateDTO bookingDto, int usuarioId);
        Task<bool> CheckAvailabilityAsync(int habitacionId, DateTime checkIn, DateTime checkOut);
    }
}
