using Alojamiento.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Alojamiento.Domain.Entities.Seguridad
{
    public class Rol : BaseAuditableEntity
    {
        public int RolId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
    }

    public class Usuario : BaseAuditableEntity
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? FotoUrl { get; set; }
        public bool EmailVerificado { get; set; } = false;
        public DateTime? UltimoAcceso { get; set; }

        public ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
        public ICollection<TokenVerificacion> Tokens { get; set; } = new List<TokenVerificacion>();
        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }

    public class UsuarioRol
    {
        public int UsuarioRolId { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public Usuario? Usuario { get; set; }
        public Rol? Rol { get; set; }
    }

    public class Cliente : BaseAuditableEntity
    {
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public int? CiudadId { get; set; }
        public string? Domicilio { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public decimal Calificacion { get; set; } = 5.00m;
        public int TotalReservas { get; set; } = 0;

        public Usuario? Usuario { get; set; }
        public Geografico.Ciudad? Ciudad { get; set; }
    }

    public class Anfitrion : BaseAuditableEntity
    {
        public int AnfitrionId { get; set; }
        public int UsuarioId { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? DocumentoFiscal { get; set; }
        public string? CuentaBancaria { get; set; }
        public bool Verificado { get; set; } = false;

        public Usuario? Usuario { get; set; }
    }

    public class TokenVerificacion
    {
        public int TokenVerificacionId { get; set; }
        public int UsuarioId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // RegistroCuenta, RecuperarPassword...
        public DateTime FechaExpiracion { get; set; }
        public bool Usado { get; set; } = false;
        public DateTime? FechaUso { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public Usuario? Usuario { get; set; }
    }

    public class Notificacion
    {
        public long NotificacionId { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public bool Leida { get; set; } = false;
        public DateTime? FechaLectura { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public Usuario? Usuario { get; set; }
    }
}
