using System;

namespace Alojamiento.Domain.Entities
{
    public abstract class BaseAuditableEntity
    {
        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public bool EliminadoLogico { get; set; } = false;
        public string? IpOrigen { get; set; }
    }
}
