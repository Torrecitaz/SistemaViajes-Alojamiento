using Alojamiento.Domain.Entities;
using System;
using System.Collections.Generic;
using Alojamiento.Domain.Entities.Geografico;
using Alojamiento.Domain.Entities.Seguridad;
using Alojamiento.Domain.Entities.Alojamientos;
using Alojamiento.Domain.Entities.Pagos;

namespace Alojamiento.Domain.Entities.Reservas
{
    public class Reserva : BaseAuditableEntity
    {
        public int ReservaId { get; set; }
        public int ClienteId { get; set; }
        public int PropiedadId { get; set; }
        public string CodigoReserva { get; set; } = string.Empty;
        public DateTime FechaCheckIn { get; set; }
        public DateTime FechaCheckOut { get; set; }
        public int NumAdultos { get; set; } = 1;
        public int NumNinos { get; set; } = 0;
        public bool LlevaMascotas { get; set; } = false;
        public int NumHabitaciones { get; set; } = 1;
        public int MonedaId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string EstadoReserva { get; set; } = "Pendiente"; // Renamed to avoid hiding base property

        public Cliente? Cliente { get; set; }
        public Propiedad? Propiedad { get; set; }
        public Moneda? Moneda { get; set; }

        public CancelacionReserva? Cancelacion { get; set; }
        public ResenaPropiedad? ResenaPropiedad { get; set; }
        public ResenaCliente? ResenaCliente { get; set; }
        public EncuestaExperiencia? Encuesta { get; set; }
        
        public ICollection<ReservaEstadoHistorial> HistorialEstados { get; set; } = new List<ReservaEstadoHistorial>();
        public ICollection<ReservaHabitacionDetalle> DetallesHabitacion { get; set; } = new List<ReservaHabitacionDetalle>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }

    public class ReservaEstadoHistorial
    {
        public long ReservaEstadoHistorialId { get; set; }
        public int ReservaId { get; set; }
        public string? EstadoAnterior { get; set; }
        public string EstadoNuevo { get; set; } = string.Empty;
        public string? Motivo { get; set; }
        public string? UsuarioAccion { get; set; }
        public DateTime FechaAccion { get; set; } = DateTime.UtcNow;
        public string? IpOrigen { get; set; }

        public Reserva? Reserva { get; set; }
    }

    public class ReservaHabitacionDetalle
    {
        public int ReservaHabitacionDetalleId { get; set; }
        public int ReservaId { get; set; }
        public int HabitacionId { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public int NumNoches { get; set; }
        public decimal SubTotalHabitacion { get; set; }

        public Reserva? Reserva { get; set; }
        public Habitacion? Habitacion { get; set; }
    }

    public class CancelacionReserva
    {
        public int CancelacionReservaId { get; set; }
        public int ReservaId { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public decimal Penalizacion { get; set; } = 0m;
        public DateTime FechaCancelacion { get; set; } = DateTime.UtcNow;
        public string? UsuarioAccion { get; set; }
        public string? IpOrigen { get; set; }

        public Reserva? Reserva { get; set; }
    }

    public class ResenaPropiedad
    {
        public int ResenaPropiedadId { get; set; }
        public int ClienteId { get; set; }
        public int PropiedadId { get; set; }
        public int ReservaId { get; set; }
        public decimal Puntuacion { get; set; }
        public string? Comentario { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string? UsuarioCreacion { get; set; }
        public bool EliminadoLogico { get; set; } = false;
        public string? IpOrigen { get; set; }

        public Cliente? Cliente { get; set; }
        public Propiedad? Propiedad { get; set; }
        public Reserva? Reserva { get; set; }
    }

    public class ResenaCliente
    {
        public int ResenaClienteId { get; set; }
        public int AnfitrionId { get; set; }
        public int ClienteId { get; set; }
        public int ReservaId { get; set; }
        public decimal Puntuacion { get; set; }
        public string? Comentario { get; set; }
        public bool EsNoShow { get; set; } = false;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string? UsuarioCreacion { get; set; }
        public bool EliminadoLogico { get; set; } = false;
        public string? IpOrigen { get; set; }

        public Anfitrion? Anfitrion { get; set; }
        public Cliente? Cliente { get; set; }
        public Reserva? Reserva { get; set; }
    }

    public class AdvertenciaCliente
    {
        public int AdvertenciaClienteId { get; set; }
        public int ClienteId { get; set; }
        public int? ReservaId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int Severidad { get; set; } = 1;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string? UsuarioCreacion { get; set; }
        public bool EliminadoLogico { get; set; } = false;
        public string? IpOrigen { get; set; }

        public Cliente? Cliente { get; set; }
        public Reserva? Reserva { get; set; }
    }

    public class EncuestaExperiencia
    {
        public int EncuestaExperienciaId { get; set; }
        public int ClienteId { get; set; }
        public int ReservaId { get; set; }
        public int PropiedadId { get; set; }
        public decimal CalificacionGeneral { get; set; }
        public decimal? Limpieza { get; set; }
        public decimal? Ubicacion { get; set; }
        public decimal? Servicio { get; set; }
        public decimal? RelacionCalidadPrecio { get; set; }
        public string? Comentarios { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string? UsuarioCreacion { get; set; }
        public bool EliminadoLogico { get; set; } = false;
        public string? IpOrigen { get; set; }

        public Cliente? Cliente { get; set; }
        public Reserva? Reserva { get; set; }
        public Propiedad? Propiedad { get; set; }
    }
}
