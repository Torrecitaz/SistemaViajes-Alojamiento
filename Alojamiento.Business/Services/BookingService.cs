using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Entities.Reservas;
using Alojamiento.Domain.Entities.Alojamientos;
using Alojamiento.Domain.Entities.Seguridad;

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
            // Verificar que no haya reservas que se crucen
            var overlaps = await _context.Reservas
                .AnyAsync(r => r.DetallesHabitacion.Any(d => d.HabitacionId == habitacionId)
                            && r.EstadoReserva != "Cancelada"
                            && r.FechaCheckIn < checkOut
                            && r.FechaCheckOut > checkIn);

            return !overlaps;
        }

        public async Task<Reserva> CreateBookingAsync(BookingCreateDTO dto, int usuarioId)
        {
            // 1. Buscar la habitación y su propiedad
            // If habitacionId is 0, find the first room of the property
            Habitacion? habitacion;
            if (dto.HabitacionId <= 0 && dto.PropiedadId > 0)
            {
                habitacion = await _context.Habitaciones
                    .Include(h => h.Tarifas)
                    .Include(h => h.Propiedad)
                    .FirstOrDefaultAsync(h => h.PropiedadId == dto.PropiedadId);
            }
            else
            {
                habitacion = await _context.Habitaciones
                    .Include(h => h.Tarifas)
                    .Include(h => h.Propiedad)
                    .FirstOrDefaultAsync(h => h.HabitacionId == dto.HabitacionId);
            }

            if (habitacion == null)
                throw new InvalidOperationException("No se encontró habitación disponible para esta propiedad.");

            // 2. Verificar disponibilidad
            bool isAvailable = await CheckAvailabilityAsync(habitacion.HabitacionId, dto.FechaCheckIn, dto.FechaCheckOut);
            if (!isAvailable)
                throw new InvalidOperationException("La habitación no está disponible para las fechas seleccionadas.");

            // 3. Buscar o crear perfil Cliente
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
            if (cliente == null)
            {
                cliente = new Cliente { UsuarioId = usuarioId };
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
            }

            // 4. Calcular costos
            int days = Math.Max(1, (dto.FechaCheckOut - dto.FechaCheckIn).Days);
            decimal precioPorNoche = habitacion.Tarifas.FirstOrDefault()?.PrecioPorNoche ?? 0m;
            decimal totalCost = precioPorNoche * days;

            // 5. Obtener moneda USD
            var moneda = await _context.Monedas.FirstOrDefaultAsync(m => m.Codigo == "USD");

            // 6. Generar código de reserva
            var codigoReserva = "RES-" + DateTime.UtcNow.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString("N")[..6].ToUpper();

            // 7. Crear la reserva
            var reserva = new Reserva
            {
                ClienteId = cliente.ClienteId,
                PropiedadId = habitacion.PropiedadId,
                CodigoReserva = codigoReserva,
                FechaCheckIn = DateTime.SpecifyKind(dto.FechaCheckIn, DateTimeKind.Utc),
                FechaCheckOut = DateTime.SpecifyKind(dto.FechaCheckOut, DateTimeKind.Utc),
                NumAdultos = dto.CantidadAdultos,
                NumNinos = dto.CantidadNinos,
                LlevaMascotas = dto.LlevaMascotas,
                MonedaId = moneda?.MonedaId ?? 1,
                SubTotal = totalCost,
                Total = totalCost,
                EstadoReserva = "Confirmada",
                DetallesHabitacion = new List<ReservaHabitacionDetalle>
                {
                    new ReservaHabitacionDetalle
                    {
                        HabitacionId = dto.HabitacionId,
                        PrecioPorNoche = precioPorNoche,
                        NumNoches = days,
                        SubTotalHabitacion = totalCost
                    }
                }
            };

            _context.Reservas.Add(reserva);

            // 8. Crear notificación
            _context.Notificaciones.Add(new Notificacion
            {
                UsuarioId = usuarioId,
                Titulo = "Reserva Confirmada",
                Mensaje = $"Tu reserva {codigoReserva} ha sido confirmada por ${totalCost} USD ({days} noches).",
                Tipo = "ReservaConfirmada"
            });

            await _context.SaveChangesAsync();

            return reserva;
        }

        // Overload for backward compatibility
        public async Task<Reserva> CreateBookingAsync(BookingCreateDTO dto)
        {
            return await CreateBookingAsync(dto, 0);
        }
    }
}
