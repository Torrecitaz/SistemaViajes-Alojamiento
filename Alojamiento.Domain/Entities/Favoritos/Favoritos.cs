using Alojamiento.Domain.Entities.Seguridad;
using Alojamiento.Domain.Entities.Alojamientos;
using System;

namespace Alojamiento.Domain.Entities.Favoritos
{
    public class ClienteFavoritoPropiedad
    {
        public int ClienteId { get; set; }
        public int PropiedadId { get; set; }
        public DateTime FechaAgregado { get; set; } = DateTime.UtcNow;

        public Cliente? Cliente { get; set; }
        public Propiedad? Propiedad { get; set; }
    }
}
