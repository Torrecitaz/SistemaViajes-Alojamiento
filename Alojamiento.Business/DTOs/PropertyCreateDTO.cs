using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Alojamiento.Business.DTOs
{
    public class PropertyCreateDTO
    {
        [Required(ErrorMessage = "El nombre de la propiedad es requerido.")]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción de la propiedad es requerida.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es requerida.")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es requerida.")]
        public int CiudadId { get; set; }

        [Required(ErrorMessage = "El tipo de alojamiento es requerido.")]
        public int TipoAlojamientoId { get; set; }

        [Required(ErrorMessage = "El precio por noche es requerido.")]
        [Range(1, 100000, ErrorMessage = "El precio debe ser mayor a cero.")]
        public decimal PrecioPorNoche { get; set; }

        [Required(ErrorMessage = "La capacidad es requerida.")]
        [Range(1, 50, ErrorMessage = "La capacidad debe ser entre 1 y 50.")]
        public int CapacidadAdultos { get; set; }

        // Archivos para las fotos
        public IFormFileCollection? Fotos { get; set; }
    }
}
