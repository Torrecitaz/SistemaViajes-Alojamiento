using Alojamiento.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Alojamiento.Domain.Entities.Geografico
{
    public class Pais : BaseAuditableEntity
    {
        public int PaisId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string CodigoISO2 { get; set; } = string.Empty;
        public string? CodigoISO3 { get; set; }
        
        // Navigation
        public ICollection<Ciudad> Ciudades { get; set; } = new List<Ciudad>();
        public ICollection<PaisMoneda> PaisesMonedas { get; set; } = new List<PaisMoneda>();
    }

    public class Ciudad : BaseAuditableEntity
    {
        public int CiudadId { get; set; }
        public int PaisId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool EsCapital { get; set; } = false;

        // Navigation
        public Pais? Pais { get; set; }
    }

    public class Moneda : BaseAuditableEntity
    {
        public int MonedaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Simbolo { get; set; } = string.Empty;

        // Navigation
        public ICollection<PaisMoneda> PaisesMonedas { get; set; } = new List<PaisMoneda>();
    }

    public class PaisMoneda
    {
        public int PaisMonedaId { get; set; }
        public int PaisId { get; set; }
        public int MonedaId { get; set; }
        public bool EsPrincipal { get; set; } = true;
        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Navigation
        public Pais? Pais { get; set; }
        public Moneda? Moneda { get; set; }
    }

    public class TasaCambio : BaseAuditableEntity
    {
        public int TasaCambioId { get; set; }
        public int MonedaOrigenId { get; set; }
        public int MonedaDestinoId { get; set; }
        public decimal Tasa { get; set; }
        public DateTime FechaVigencia { get; set; }

        // Navigation
        public Moneda? MonedaOrigen { get; set; }
        public Moneda? MonedaDestino { get; set; }
    }
}
