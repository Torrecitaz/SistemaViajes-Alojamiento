using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alojamiento.Business.Interfaces;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Enums;
using Alojamiento.Domain.Entities.Marketing;

namespace Alojamiento.Business.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly AlojamientoDbContext _context;

        public CollaboratorService(AlojamientoDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetDashboardMetricsAsync(int collaboratorId)
        {
            var properties = await _context.Propiedades
                .Where(p => p.AnfitrionId == collaboratorId)
                .Select(p => p.PropiedadId)
                .ToListAsync();

            var reservations = await _context.Reservas
                .Include(r => r.DetallesHabitacion)
                .ThenInclude(d => d.Habitacion)
                .Where(r => r.DetallesHabitacion.Any(d => d.Habitacion != null && properties.Contains(d.Habitacion.PropiedadId)))
                .ToListAsync();

            return new
            {
                TotalProperties = properties.Count,
                TotalReservations = reservations.Count,
                ConfirmedReservations = reservations.Count(r => r.EstadoReserva == "Confirmada"),
                TotalEarnings = reservations.Where(r => r.EstadoReserva == "Confirmada").Sum(r => r.Total)
            };
        }

        public async Task ReportNoShowAsync(int reservaId)
        {
            var reserva = await _context.Reservas.FindAsync(reservaId);
            if (reserva == null) throw new Exception("Reserva no encontrada.");

            reserva.EstadoReserva = "NoShow";

            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == reserva.ClienteId);
            if (cliente != null)
            {
                var advertencia = new Alojamiento.Domain.Entities.Reservas.AdvertenciaCliente
                {
                    ClienteId = cliente.ClienteId,
                    ReservaId = reserva.ReservaId,
                    Tipo = "NoShow",
                    Descripcion = "El cliente no se presentó a la reserva."
                };
                _context.AdvertenciasClientes.Add(advertencia);

                // Penalizar reputación
                cliente.Calificacion -= 0.5m;
                if (cliente.Calificacion < 0) cliente.Calificacion = 0;
            }

            await _context.SaveChangesAsync();
        }

        public async Task ApplyPromotionAsync(int propiedadId, int puntosCosto)
        {
            var property = await _context.Propiedades.FindAsync(propiedadId);
            if (property == null) throw new Exception("Propiedad no encontrada.");

            var puntos = await _context.PuntosAnfitriones.FirstOrDefaultAsync(p => p.AnfitrionId == property.AnfitrionId);
            if (puntos == null || puntos.PuntosAcumulados < puntosCosto)
                throw new Exception("Puntos insuficientes.");

            puntos.PuntosAcumulados -= puntosCosto;
            
            var historial = new HistorialPuntosAnfitrion 
            {
                PuntosAnfitrionId = puntos.PuntosAnfitrionId,
                Cantidad = -puntosCosto,
                TipoTransaccion = "Canje",
                Descripcion = $"Promoción activada para la propiedad {property.Nombre}"
            };
            _context.HistorialPuntosAnfitriones.Add(historial);

            var promocion = new Promocion
            {
                PropiedadId = propiedadId,
                Nombre = "Promoción de Visibilidad Especial",
                PorcentajeDescuento = 0, // Promocion de visibilidad, no afecta precio directo
                FechaInicio = DateTime.UtcNow,
                FechaFin = DateTime.UtcNow.AddDays(7),
                Activa = true
            };
            _context.Promociones.Add(promocion);
            await _context.SaveChangesAsync();
        }
    }
}
