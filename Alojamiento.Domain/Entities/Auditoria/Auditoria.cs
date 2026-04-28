using Alojamiento.Domain.Entities;
using System;

namespace Alojamiento.Domain.Entities.Auditoria
{
    public class AuditoriaGeneral
    {
        public long AuditoriaGeneralId { get; set; }
        public string NombreEsquema { get; set; } = string.Empty;
        public string NombreTabla { get; set; } = string.Empty;
        public string Operacion { get; set; } = string.Empty;
        public string RegistroId { get; set; } = string.Empty;
        public string? DatosAnteriores { get; set; }
        public string? DatosNuevos { get; set; }
        public string? UsuarioAccion { get; set; }
        public DateTime FechaAccion { get; set; } = DateTime.UtcNow;
        public string? IpOrigen { get; set; }
    }

    public class ErrorAplicacion
    {
        public long ErrorAplicacionId { get; set; }
        public string? Modulo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string? Detalle { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime FechaError { get; set; } = DateTime.UtcNow;
        public string? IpOrigen { get; set; }

        public Seguridad.Usuario? Usuario { get; set; }
    }

    public class AccesoUsuario
    {
        public long AccesoUsuarioId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaAcceso { get; set; } = DateTime.UtcNow;
        public string? IpOrigen { get; set; }
        public string? UserAgent { get; set; }
        public bool Exitoso { get; set; }
        public string? Mensaje { get; set; }

        public Seguridad.Usuario? Usuario { get; set; }
    }
}
