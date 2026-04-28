using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alojamiento.Business.Interfaces;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Entities.Pagos;
using Alojamiento.Domain.Entities.Seguridad;

namespace Alojamiento.Business.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AlojamientoDbContext _context;

        public PaymentService(AlojamientoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessPaymentAsync(int reservaId, decimal amount, string currency)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .ThenInclude(c => c.Usuario)
                .FirstOrDefaultAsync(r => r.ReservaId == reservaId);

            if (reserva == null) throw new Exception("Reserva no encontrada.");

            var moneda = await _context.Monedas.FirstOrDefaultAsync(m => m.Codigo == currency.ToUpper());
            int monedaId = moneda?.MonedaId ?? 1; // Default fallback si no encuentra la moneda

            // 1. Simular Pago
            var pago = new Pago
            {
                ReservaId = reserva.ReservaId,
                Monto = amount,
                MonedaId = monedaId,
                TipoPago = "Tarjeta de Crédito",
                EstadoPago = "Procesado",
                ReferenciaPago = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(),
                FechaPago = DateTime.UtcNow
            };
            _context.Pagos.Add(pago);

            // 2. Generar Factura
            var factura = new Factura
            {
                Pago = pago,
                NumeroFactura = $"FAC-{DateTime.UtcNow:yyyyMMdd}-{reservaId}",
                NombreFacturacion = reserva.Cliente?.Usuario?.NombreCompleto ?? "Consumidor Final",
                DocumentoFacturacion = "999999999", // Dummy
                SubTotal = amount * 0.85m, // Simulando 15% de impuestos
                Impuesto = amount * 0.15m,
                Total = amount,
                EstadoFactura = "Emitida"
            };
            _context.Facturas.Add(factura);

            // 3. Generar Notificación para el cliente
            if (reserva.Cliente != null)
            {
                var notificacion = new Notificacion
                {
                    UsuarioId = reserva.Cliente.UsuarioId,
                    Titulo = "Pago Confirmado",
                    Mensaje = $"Hemos recibido tu pago de {amount} {currency} por tu reserva. ¡Todo listo!",
                    Tipo = "PagoConfirmado"
                };
                _context.Notificaciones.Add(notificacion);
            }

            // 4. Actualizar Estado de la Reserva
            reserva.EstadoReserva = "Confirmada";

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
