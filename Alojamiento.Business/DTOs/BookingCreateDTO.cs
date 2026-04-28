using System;
using System.ComponentModel.DataAnnotations;
using Alojamiento.Domain.Enums;

namespace Alojamiento.Business.DTOs
{
    public class BookingCreateDTO
    {
        [Required]
        public int ClienteId { get; set; }
        
        [Required]
        public int HabitacionId { get; set; }

        [Required]
        public DateTime FechaCheckIn { get; set; }

        [Required]
        public DateTime FechaCheckOut { get; set; }

        [Range(1, 10)]
        public int CantidadAdultos { get; set; }

        [Range(0, 10)]
        public int CantidadNiños { get; set; }

        public bool LlevaMascotas { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string EmailConfirmacion { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "El teléfono no es válido.")]
        public string TelefonoContacto { get; set; } = string.Empty;

        public MetodoPago MetodoPago { get; set; } = MetodoPago.Tarjeta;
    }
}
