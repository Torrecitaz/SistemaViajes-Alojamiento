using System;
using System.ComponentModel.DataAnnotations;

namespace Alojamiento.Business.DTOs
{
    public class BookingCreateDTO
    {
        [Required]
        public int PropiedadId { get; set; }

        [Required]
        public int HabitacionId { get; set; }

        [Required]
        public DateTime FechaCheckIn { get; set; }

        [Required]
        public DateTime FechaCheckOut { get; set; }

        [Range(1, 20)]
        public int CantidadAdultos { get; set; } = 2;

        [Range(0, 10)]
        public int CantidadNinos { get; set; } = 0;

        public bool LlevaMascotas { get; set; } = false;

        public string? EmailConfirmacion { get; set; }
        public string? TelefonoContacto { get; set; }
    }
}
