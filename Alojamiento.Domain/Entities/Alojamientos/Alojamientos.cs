using Alojamiento.Domain.Entities;
using System;
using System.Collections.Generic;
using Alojamiento.Domain.Entities.Geografico;
using Alojamiento.Domain.Entities.Seguridad;

namespace Alojamiento.Domain.Entities.Alojamientos
{
    public class TipoAlojamiento : BaseAuditableEntity
    {
        public int TipoAlojamientoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public ICollection<Propiedad> Propiedades { get; set; } = new List<Propiedad>();
    }

    public class Propiedad : BaseAuditableEntity
    {
        public int PropiedadId { get; set; }
        public int AnfitrionId { get; set; }
        public int TipoAlojamientoId { get; set; }
        public int CiudadId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public int? Estrellas { get; set; }
        public decimal CalificacionPromedio { get; set; } = 0m;
        public int TotalResenas { get; set; } = 0;
        public bool AdmiteMascotas { get; set; } = false;
        public bool Verificada { get; set; } = false;
        public string EstadoPropiedad { get; set; } = "Pendiente"; // Renamed from Estado (conflict with BaseEntity)

        public Anfitrion? Anfitrion { get; set; }
        public TipoAlojamiento? TipoAlojamiento { get; set; }
        public Ciudad? Ciudad { get; set; }

        public PropiedadPolitica? Politica { get; set; }
        public ICollection<PropiedadFoto> Fotos { get; set; } = new List<PropiedadFoto>();
        public ICollection<PropiedadServicio> Servicios { get; set; } = new List<PropiedadServicio>();
        public ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
    }

    public class PropiedadPolitica : BaseAuditableEntity
    {
        public int PropiedadPoliticaId { get; set; }
        public int PropiedadId { get; set; }
        public TimeSpan HoraCheckIn { get; set; }
        public TimeSpan HoraCheckOut { get; set; }
        public bool PermiteMascotas { get; set; } = false;
        public bool PermiteNinos { get; set; } = true;
        public string? PoliticaCancelacion { get; set; }
        public string? ReglasCasa { get; set; }
        public int? EdadMinimaReserva { get; set; }

        public Propiedad? Propiedad { get; set; }
    }

    public class PropiedadFoto
    {
        public int PropiedadFotoId { get; set; }
        public int PropiedadId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsPrincipal { get; set; } = false;
        public int Orden { get; set; } = 0;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string? UsuarioCreacion { get; set; }
        public bool EliminadoLogico { get; set; } = false;

        public Propiedad? Propiedad { get; set; }
    }

    public class Servicio : BaseAuditableEntity
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Icono { get; set; }

        public ICollection<PropiedadServicio> PropiedadesServicios { get; set; } = new List<PropiedadServicio>();
    }

    public class PropiedadServicio
    {
        public int PropiedadServicioId { get; set; }
        public int PropiedadId { get; set; }
        public int ServicioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public Propiedad? Propiedad { get; set; }
        public Servicio? Servicio { get; set; }
    }

    public class Habitacion : BaseAuditableEntity
    {
        public int HabitacionId { get; set; }
        public int PropiedadId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int CapacidadAdultos { get; set; } = 2;
        public int CapacidadNinos { get; set; } = 0;
        public int NumBanos { get; set; } = 1;
        public int NumDormitorios { get; set; } = 1;
        public bool AdmiteMascotas { get; set; } = false;
        public bool TieneCocina { get; set; } = false;
        public bool TieneAireAcondicionado { get; set; } = false;
        public decimal? SuperficieM2 { get; set; }

        public Propiedad? Propiedad { get; set; }
        
        public ICollection<HabitacionFoto> Fotos { get; set; } = new List<HabitacionFoto>();
        public ICollection<TarifaHabitacion> Tarifas { get; set; } = new List<TarifaHabitacion>();
        public ICollection<DisponibilidadHabitacion> Disponibilidades { get; set; } = new List<DisponibilidadHabitacion>();
    }

    public class HabitacionFoto
    {
        public int HabitacionFotoId { get; set; }
        public int HabitacionId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsPrincipal { get; set; } = false;
        public int Orden { get; set; } = 0;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public bool EliminadoLogico { get; set; } = false;

        public Habitacion? Habitacion { get; set; }
    }

    public class TarifaHabitacion : BaseAuditableEntity
    {
        public int TarifaHabitacionId { get; set; }
        public int HabitacionId { get; set; }
        public int MonedaId { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public Habitacion? Habitacion { get; set; }
        public Moneda? Moneda { get; set; }
    }

    public class DisponibilidadHabitacion
    {
        public int DisponibilidadHabitacionId { get; set; }
        public int HabitacionId { get; set; }
        public DateTime Fecha { get; set; }
        public bool Disponible { get; set; } = true;
        public DateTime? FechaModificacion { get; set; }

        public Habitacion? Habitacion { get; set; }
    }
}
