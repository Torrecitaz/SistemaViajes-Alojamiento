using System.Threading.Tasks;
using Alojamiento.Business.DTOs;
using Alojamiento.Domain.Entities;

namespace Alojamiento.Business.Interfaces
{
    public interface IBookingService
    {
        Task<Reserva> CreateBookingAsync(BookingCreateDTO bookingDto);
        Task<bool> CheckAvailabilityAsync(int habitacionId, System.DateTime checkIn, System.DateTime checkOut);
    }
}
