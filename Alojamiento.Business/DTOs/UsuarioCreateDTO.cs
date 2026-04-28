using System.ComponentModel.DataAnnotations;

namespace Alojamiento.Business.DTOs
{
    public class UsuarioCreateDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "El teléfono no es válido.")]
        public string Telefono { get; set; } = string.Empty;

        public string FotoPerfilUrl { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        
        // Note: No se incluye EsColaborador para evitar Mass Assignment (TC-S20)
    }
}
