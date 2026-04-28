using Alojamiento.Business.Interfaces;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alojamiento.Business.Services
{
    public class AlojamientoService : IAlojamientoService
    {
        private readonly AlojamientoDbContext _context;

        public AlojamientoService(AlojamientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Propiedad>> ObtenerPropiedadesAsync()
        {
            return await _context.Propiedades.ToListAsync();
        }

        public async Task<Propiedad> CrearPropiedadAsync(Propiedad propiedad)
        {
            _context.Propiedades.Add(propiedad);
            await _context.SaveChangesAsync();
            return propiedad;
        }

        public async Task<Reserva> ProcesarReservaAsync(Reserva reserva)
        {
            // Regla de Negocio: Validar fechas
            if (reserva.FechaCheckOut <= reserva.FechaCheckIn)
                throw new Exception("La fecha de salida debe ser posterior a la de entrada.");

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return reserva;
        }
    }
}