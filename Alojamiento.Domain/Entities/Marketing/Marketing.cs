using Alojamiento.Domain.Entities.Seguridad;
using Alojamiento.Domain.Entities.Alojamientos;
using System;
using System.Collections.Generic;

namespace Alojamiento.Domain.Entities.Marketing
{
    public class PuntosCliente : BaseAuditableEntity
    {
        public int PuntosClienteId { get; set; }
        public int ClienteId { get; set; }
        public int PuntosAcumulados { get; set; } = 0;

        public Cliente? Cliente { get; set; }
        public ICollection<HistorialPuntosCliente> Historial { get; set; } = new List<HistorialPuntosCliente>();
    }

    public class HistorialPuntosCliente
    {
        public long HistorialPuntosClienteId { get; set; }
        public int PuntosClienteId { get; set; }
        public int Cantidad { get; set; }
        public string TipoTransaccion { get; set; } = string.Empty; // "Acumulacion", "Canje"
        public string? Descripcion { get; set; }
        public DateTime FechaTransaccion { get; set; } = DateTime.UtcNow;

        public PuntosCliente? PuntosCliente { get; set; }
    }

    public class PuntosAnfitrion : BaseAuditableEntity
    {
        public int PuntosAnfitrionId { get; set; }
        public int AnfitrionId { get; set; }
        public int PuntosAcumulados { get; set; } = 0;

        public Anfitrion? Anfitrion { get; set; }
        public ICollection<HistorialPuntosAnfitrion> Historial { get; set; } = new List<HistorialPuntosAnfitrion>();
    }

    public class HistorialPuntosAnfitrion
    {
        public long HistorialPuntosAnfitrionId { get; set; }
        public int PuntosAnfitrionId { get; set; }
        public int Cantidad { get; set; }
        public string TipoTransaccion { get; set; } = string.Empty; // "Acumulacion", "Canje"
        public string? Descripcion { get; set; }
        public DateTime FechaTransaccion { get; set; } = DateTime.UtcNow;

        public PuntosAnfitrion? PuntosAnfitrion { get; set; }
    }

    public class Promocion : BaseAuditableEntity
    {
        public int PromocionId { get; set; }
        public int PropiedadId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activa { get; set; } = true;

        public Propiedad? Propiedad { get; set; }
    }
}
