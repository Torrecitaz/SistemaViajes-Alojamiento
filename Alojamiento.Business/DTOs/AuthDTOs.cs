using System.ComponentModel.DataAnnotations;

namespace Alojamiento.Business.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        public string Telefono { get; set; } = string.Empty;

        public bool EsAnfitrion { get; set; } = false;
    }

    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public UserDetailsDTO User { get; set; } = new UserDetailsDTO();
    }

    public class UserDetailsDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; // "CLIENTE" o "ANFITRION" o "ADMIN"
    }
}
