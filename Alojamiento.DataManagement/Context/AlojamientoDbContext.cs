using Alojamiento.Domain.Entities;
using Alojamiento.Domain.Entities.Geografico;
using Alojamiento.Domain.Entities.Seguridad;
using Alojamiento.Domain.Entities.Alojamientos;
using Alojamiento.Domain.Entities.Reservas;
using Alojamiento.Domain.Entities.Pagos;
using Alojamiento.Domain.Entities.Auditoria;
using Alojamiento.Domain.Entities.Marketing;
using Alojamiento.Domain.Entities.Favoritos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alojamiento.DataManagement.Context
{
    public class AlojamientoDbContext : DbContext
    {
        public AlojamientoDbContext(DbContextOptions<AlojamientoDbContext> options) : base(options) { }

        // Módulo Geográfico
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<PaisMoneda> PaisMonedas { get; set; }
        public DbSet<TasaCambio> TasasCambios { get; set; }

        // Módulo Seguridad
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Anfitrion> Anfitriones { get; set; }
        public DbSet<TokenVerificacion> TokensVerificacion { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        // Módulo Alojamientos
        public DbSet<TipoAlojamiento> TiposAlojamiento { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<PropiedadPolitica> PropiedadesPoliticas { get; set; }
        public DbSet<PropiedadFoto> PropiedadesFotos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<PropiedadServicio> PropiedadesServicios { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<HabitacionFoto> HabitacionesFotos { get; set; }
        public DbSet<TarifaHabitacion> TarifasHabitaciones { get; set; }
        public DbSet<DisponibilidadHabitacion> DisponibilidadesHabitaciones { get; set; }

        // Módulo Reservas
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ReservaEstadoHistorial> ReservaEstadosHistorial { get; set; }
        public DbSet<ReservaHabitacionDetalle> ReservaHabitacionDetalles { get; set; }
        public DbSet<CancelacionReserva> CancelacionesReservas { get; set; }
        public DbSet<ResenaPropiedad> ResenasPropiedades { get; set; }
        public DbSet<ResenaCliente> ResenasClientes { get; set; }
        public DbSet<AdvertenciaCliente> AdvertenciasClientes { get; set; }
        public DbSet<EncuestaExperiencia> EncuestasExperiencia { get; set; }

        // Módulo Pagos
        public DbSet<MetodoPagoCliente> MetodosPagosClientes { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<PagoEstadoHistorial> PagoEstadosHistorial { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        // Módulo Marketing
        public DbSet<PuntosCliente> PuntosClientes { get; set; }
        public DbSet<HistorialPuntosCliente> HistorialPuntosClientes { get; set; }
        public DbSet<PuntosAnfitrion> PuntosAnfitriones { get; set; }
        public DbSet<HistorialPuntosAnfitrion> HistorialPuntosAnfitriones { get; set; }
        public DbSet<Promocion> Promociones { get; set; }

        // Módulo Favoritos
        public DbSet<ClienteFavoritoPropiedad> ClientesFavoritosPropiedades { get; set; }

        // Módulo Auditoría
        public DbSet<AuditoriaGeneral> AuditoriaGeneral { get; set; }
        public DbSet<ErrorAplicacion> ErroresAplicacion { get; set; }
        public DbSet<AccesoUsuario> AccesosUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // geo
            modelBuilder.Entity<Pais>().ToTable("Pais", "geo");
            modelBuilder.Entity<Ciudad>().ToTable("Ciudad", "geo");
            modelBuilder.Entity<Moneda>().ToTable("Moneda", "geo");
            modelBuilder.Entity<PaisMoneda>().ToTable("PaisMoneda", "geo");
            modelBuilder.Entity<TasaCambio>().ToTable("TasaCambio", "geo");

            // seg
            modelBuilder.Entity<Rol>().ToTable("Rol", "seg");
            modelBuilder.Entity<Usuario>().ToTable("Usuario", "seg");
            modelBuilder.Entity<UsuarioRol>().ToTable("UsuarioRol", "seg");
            modelBuilder.Entity<Cliente>().ToTable("Cliente", "seg");
            modelBuilder.Entity<Anfitrion>().ToTable("Anfitrion", "seg");
            modelBuilder.Entity<TokenVerificacion>().ToTable("TokenVerificacion", "seg");
            modelBuilder.Entity<Notificacion>().ToTable("Notificacion", "seg");

            // alo
            modelBuilder.Entity<TipoAlojamiento>().ToTable("TipoAlojamiento", "alo");
            modelBuilder.Entity<Propiedad>().ToTable("Propiedad", "alo");
            modelBuilder.Entity<PropiedadPolitica>().ToTable("PropiedadPolitica", "alo");
            modelBuilder.Entity<PropiedadFoto>().ToTable("PropiedadFoto", "alo");
            modelBuilder.Entity<Servicio>().ToTable("Servicio", "alo");
            modelBuilder.Entity<PropiedadServicio>().ToTable("PropiedadServicio", "alo");
            modelBuilder.Entity<Habitacion>().ToTable("Habitacion", "alo");
            modelBuilder.Entity<HabitacionFoto>().ToTable("HabitacionFoto", "alo");
            modelBuilder.Entity<TarifaHabitacion>().ToTable("TarifaHabitacion", "alo");
            modelBuilder.Entity<DisponibilidadHabitacion>().ToTable("DisponibilidadHabitacion", "alo");

            // res
            modelBuilder.Entity<Reserva>().ToTable("Reserva", "res");
            modelBuilder.Entity<ReservaEstadoHistorial>().ToTable("ReservaEstadoHistorial", "res");
            modelBuilder.Entity<ReservaHabitacionDetalle>().ToTable("ReservaHabitacionDetalle", "res");
            modelBuilder.Entity<CancelacionReserva>().ToTable("CancelacionReserva", "res");
            modelBuilder.Entity<ResenaPropiedad>().ToTable("ResenaPropiedad", "res");
            modelBuilder.Entity<ResenaCliente>().ToTable("ResenaCliente", "res");
            modelBuilder.Entity<AdvertenciaCliente>().ToTable("AdvertenciaCliente", "res");
            modelBuilder.Entity<EncuestaExperiencia>().ToTable("EncuestaExperiencia", "res");

            // pag
            modelBuilder.Entity<MetodoPagoCliente>().ToTable("MetodoPagoCliente", "pag");
            modelBuilder.Entity<Pago>().ToTable("Pago", "pag");
            modelBuilder.Entity<PagoEstadoHistorial>().ToTable("PagoEstadoHistorial", "pag");
            modelBuilder.Entity<Factura>().ToTable("Factura", "pag");

            // aud
            modelBuilder.Entity<AuditoriaGeneral>().ToTable("AuditoriaGeneral", "aud");
            modelBuilder.Entity<ErrorAplicacion>().ToTable("ErrorAplicacion", "aud");
            modelBuilder.Entity<AccesoUsuario>().ToTable("AccesoUsuario", "aud");

            // mkt
            modelBuilder.Entity<PuntosCliente>().ToTable("PuntosCliente", "mkt");
            modelBuilder.Entity<HistorialPuntosCliente>().ToTable("HistorialPuntosCliente", "mkt");
            modelBuilder.Entity<PuntosAnfitrion>().ToTable("PuntosAnfitrion", "mkt");
            modelBuilder.Entity<HistorialPuntosAnfitrion>().ToTable("HistorialPuntosAnfitrion", "mkt");
            modelBuilder.Entity<Promocion>().ToTable("Promocion", "mkt");

            // fav
            modelBuilder.Entity<ClienteFavoritoPropiedad>().ToTable("ClienteFavoritoPropiedad", "fav");

            // Configurar llaves compuestas y precision
            modelBuilder.Entity<UsuarioRol>().HasKey(ur => new { ur.UsuarioId, ur.RolId });
            modelBuilder.Entity<PaisMoneda>().HasKey(pm => new { pm.PaisId, pm.MonedaId });
            modelBuilder.Entity<PropiedadServicio>().HasKey(ps => new { ps.PropiedadId, ps.ServicioId });
            modelBuilder.Entity<ClienteFavoritoPropiedad>().HasKey(cf => new { cf.ClienteId, cf.PropiedadId });
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseAuditableEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.FechaCreacion = DateTime.UtcNow;
                    entry.Entity.UsuarioCreacion = "System"; // TODO: Obtain from HttpContext User
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.FechaModificacion = DateTime.UtcNow;
                    entry.Entity.UsuarioModificacion = "System";
                }
            }
        }
    }
}