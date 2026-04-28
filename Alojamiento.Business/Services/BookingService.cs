using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;
using Alojamiento.Domain.Entities;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Enums;
using System.Linq;

namespace Alojamiento.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly AlojamientoDbContext _context;

        public BookingService(AlojamientoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckAvailabilityAsync(int habitacionId, DateTime checkIn, DateTime checkOut)
        {
            // Validar que la habitación esté marcada como disponible todos los días en DisponibilidadHabitacion
            var notAvailable = await _context.DisponibilidadesHabitaciones
                .AnyAsync(d => d.HabitacionId == habitacionId && d.Fecha >= checkIn && d.Fecha < checkOut && !d.Disponible);

            if (notAvailable) return false;

            // Validar que la habitación no tenga reservas confirmadas que se crucen con las fechas
            var overlaps = await _context.Reservas
                .AnyAsync(r => r.DetallesHabitacion.Any(d => d.HabitacionId == habitacionId) 
                            && r.EstadoReserva != "Cancelada"
                            && r.FechaCheckIn < checkOut 
                            && r.FechaCheckOut > checkIn);
            
            return !overlaps;
        }

        public async Task<Reserva> CreateBookingAsync(BookingCreateDTO bookingDto)
        {
            bool isAvailable = await CheckAvailabilityAsync(bookingDto.HabitacionId, bookingDto.FechaCheckIn, bookingDto.FechaCheckOut);
            
            if (!isAvailable)
            {
                throw new InvalidOperationException("La habitación no está disponible para las fechas seleccionadas.");
            }

            var habitacion = await _context.Habitaciones.Include(h => h.Tarifas).FirstOrDefaultAsync(h => h.HabitacionId == bookingDto.HabitacionId);
            if (habitacion == null) throw new Exception("Habitación no encontrada");

            int days = (bookingDto.FechaCheckOut - bookingDto.FechaCheckIn).Days;
            decimal precioPorNoche = habitacion.Tarifas.FirstOrDefault()?.PrecioPorNoche ?? 0m;
            decimal totalCost = precioPorNoche * days;

            var reserva = new Reserva
            {
                ClienteId = bookingDto.ClienteId,
                FechaCheckIn = bookingDto.FechaCheckIn,
                FechaCheckOut = bookingDto.FechaCheckOut,
                NumAdultos = bookingDto.CantidadAdultos,
                NumNinos = bookingDto.CantidadNiños,
                LlevaMascotas = bookingDto.LlevaMascotas,
                SubTotal = totalCost,
                Total = totalCost,
                EstadoReserva = "Pendiente",
                DetallesHabitacion = new List<ReservaHabitacionDetalle> 
                { 
                    new ReservaHabitacionDetalle 
                    { 
                        HabitacionId = bookingDto.HabitacionId, 
                        PrecioPorNoche = precioPorNoche,
                        NumNoches = days,
                        SubTotalHabitacion = totalCost
                    } 
                }
            };

            _context.Reservas.Add(reserva);

            // Bloquear disponibilidad en las fechas correspondientes
            for (var date = bookingDto.FechaCheckIn; date < bookingDto.FechaCheckOut; date = date.AddDays(1))
            {
                var disp = await _context.DisponibilidadesHabitaciones
                    .FirstOrDefaultAsync(d => d.HabitacionId == bookingDto.HabitacionId && d.Fecha.Date == date.Date);

                if (disp != null)
                {
                    disp.Disponible = false;
                    disp.FechaModificacion = DateTime.UtcNow;
                }
                else
                {
                    _context.DisponibilidadesHabitaciones.Add(new Alojamiento.Domain.Entities.Alojamientos.DisponibilidadHabitacion
                    {
                        HabitacionId = bookingDto.HabitacionId,
                        Fecha = date.Date,
                        Disponible = false,
                        FechaModificacion = DateTime.UtcNow
                    });
                }
            }

            // Crear notificación para el cliente
            var cliente = await _context.Set<Alojamiento.Domain.Entities.Seguridad.Cliente>().FirstOrDefaultAsync(c => c.UsuarioId == bookingDto.ClienteId);
            if (cliente != null)
            {
                var notificacion = new Alojamiento.Domain.Entities.Seguridad.Notificacion
                {
                    UsuarioId = cliente.UsuarioId,
                    Titulo = "Nueva Reserva Creada",
                    Mensaje = $"Tu reserva para la habitación ha sido registrada y está pendiente de pago.",
                    Tipo = "ReservaCreada"
                };
                _context.Notificaciones.Add(notificacion);
            }

            await _context.SaveChangesAsync();

            return reserva;
        }
    }
}
