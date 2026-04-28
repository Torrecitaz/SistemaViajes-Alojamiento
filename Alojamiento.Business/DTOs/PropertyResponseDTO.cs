using System.Collections.Generic;
using Alojamiento.Domain.Enums;

namespace Alojamiento.Business.DTOs
{
    public class PropertyResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public Alojamiento.Domain.Enums.TipoAlojamiento TipoAlojamiento { get; set; }
        public decimal PrecioBase { get; set; }
        public string Moneda { get; set; } = string.Empty;
        public double Calificacion { get; set; }
        public List<string> Instalaciones { get; set; } = new List<string>();
        public List<string> Fotos { get; set; } = new List<string>();
    }
}
