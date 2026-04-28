using Alojamiento.Domain.Entities;
using System;
using System.Collections.Generic;
using Alojamiento.Domain.Entities.Geografico;
using Alojamiento.Domain.Entities.Seguridad;
using Alojamiento.Domain.Entities.Reservas; // Assuming forward ref is ok

namespace Alojamiento.Domain.Entities.Pagos
{
    public class MetodoPagoCliente : BaseAuditableEntity
    {
        public int MetodoPagoClienteId { get; set; }
        public int ClienteId { get; set; }
        public string Tipo { get; set; } = string.Empty; // Tarjeta, Transferencia...
        public string? NombreTitular { get; set; }
        public string? MarcaTarjeta { get; set; }
        public string? UltimosCuatroDigitos { get; set; }
        public string? FechaExpiracion { get; set; }
        public bool EsPrincipal { get; set; } = false;

        public Cliente? Cliente { get; set; }
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }

    public class Pago : BaseAuditableEntity
    {
        public int PagoId { get; set; }
        public int ReservaId { get; set; }
        public int? MetodoPagoClienteId { get; set; }
        public string TipoPago { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public int MonedaId { get; set; }
        public string EstadoPago { get; set; } = "Pendiente"; // Renamed from Estado
        public string? ReferenciaPago { get; set; }
        public DateTime? FechaPago { get; set; }

        public Reserva? Reserva { get; set; }
        public MetodoPagoCliente? MetodoPagoCliente { get; set; }
        public Moneda? Moneda { get; set; }
        
        public Factura? Factura { get; set; }
        public ICollection<PagoEstadoHistorial> HistorialEstados { get; set; } = new List<PagoEstadoHistorial>();
    }

    public class PagoEstadoHistorial
    {
        public long PagoEstadoHistorialId { get; set; }
        public int PagoId { get; set; }
        public string? EstadoAnterior { get; set; }
        public string EstadoNuevo { get; set; } = string.Empty;
        public string? Motivo { get; set; }
        public string? UsuarioAccion { get; set; }
        public DateTime FechaAccion { get; set; } = DateTime.UtcNow;
        public string? IpOrigen { get; set; }

        public Pago? Pago { get; set; }
    }

    public class Factura : BaseAuditableEntity
    {
        public int FacturaId { get; set; }
        public int PagoId { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public string NombreFacturacion { get; set; } = string.Empty;
        public string DocumentoFacturacion { get; set; } = string.Empty;
        public string? DireccionFacturacion { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; } = 0m;
        public decimal Total { get; set; }
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
        public string EstadoFactura { get; set; } = "Emitida"; // Renamed from Estado

        public Pago? Pago { get; set; }
    }
}
