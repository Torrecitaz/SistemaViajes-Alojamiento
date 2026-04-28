using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Entities.Geografico;
using Alojamiento.Domain.Entities.Seguridad;
using Alojamiento.Domain.Entities.Alojamientos;

namespace Alojamiento.DataManagement.Seeders
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AlojamientoDbContext context)
        {
            // Aplicar migraciones pendientes
            await context.Database.MigrateAsync();

            // 1. Monedas
            if (!await context.Monedas.AnyAsync())
            {
                context.Monedas.AddRange(
                    new Moneda { Nombre = "Dólar Estadounidense", Codigo = "USD", Simbolo = "$" },
                    new Moneda { Nombre = "Euro", Codigo = "EUR", Simbolo = "€" },
                    new Moneda { Nombre = "Peso Colombiano", Codigo = "COP", Simbolo = "$" }
                );
                await context.SaveChangesAsync();
            }

            // 2. Países
            if (!await context.Paises.AnyAsync())
            {
                context.Paises.AddRange(
                    new Pais { Nombre = "Ecuador", CodigoISO2 = "EC", CodigoISO3 = "ECU" },
                    new Pais { Nombre = "Colombia", CodigoISO2 = "CO", CodigoISO3 = "COL" },
                    new Pais { Nombre = "Perú", CodigoISO2 = "PE", CodigoISO3 = "PER" },
                    new Pais { Nombre = "México", CodigoISO2 = "MX", CodigoISO3 = "MEX" }
                );
                await context.SaveChangesAsync();
            }

            // 3. Ciudades
            if (!await context.Ciudades.AnyAsync())
            {
                var ec = await context.Paises.FirstOrDefaultAsync(p => p.CodigoISO2 == "EC");
                var co = await context.Paises.FirstOrDefaultAsync(p => p.CodigoISO2 == "CO");
                var pe = await context.Paises.FirstOrDefaultAsync(p => p.CodigoISO2 == "PE");
                var mx = await context.Paises.FirstOrDefaultAsync(p => p.CodigoISO2 == "MX");

                var ciudades = new List<Ciudad>();
                if (ec != null)
                {
                    ciudades.AddRange(new[]
                    {
                        new Ciudad { Nombre = "Quito", PaisId = ec.PaisId, EsCapital = true },
                        new Ciudad { Nombre = "Guayaquil", PaisId = ec.PaisId, EsCapital = false },
                        new Ciudad { Nombre = "Cuenca", PaisId = ec.PaisId, EsCapital = false },
                        new Ciudad { Nombre = "Galápagos", PaisId = ec.PaisId, EsCapital = false }
                    });
                }
                if (co != null)
                {
                    ciudades.AddRange(new[]
                    {
                        new Ciudad { Nombre = "Bogotá", PaisId = co.PaisId, EsCapital = true },
                        new Ciudad { Nombre = "Medellín", PaisId = co.PaisId, EsCapital = false },
                        new Ciudad { Nombre = "Cartagena", PaisId = co.PaisId, EsCapital = false }
                    });
                }
                if (pe != null)
                {
                    ciudades.AddRange(new[]
                    {
                        new Ciudad { Nombre = "Lima", PaisId = pe.PaisId, EsCapital = true },
                        new Ciudad { Nombre = "Cusco", PaisId = pe.PaisId, EsCapital = false }
                    });
                }
                if (mx != null)
                {
                    ciudades.Add(new Ciudad { Nombre = "Cancún", PaisId = mx.PaisId, EsCapital = false });
                }

                context.Ciudades.AddRange(ciudades);
                await context.SaveChangesAsync();
            }

            // 4. Roles
            if (!await context.Roles.AnyAsync())
            {
                context.Roles.AddRange(
                    new Rol { Nombre = "Admin", Descripcion = "Administrador Global" },
                    new Rol { Nombre = "Anfitrion", Descripcion = "Propietario de Alojamientos" },
                    new Rol { Nombre = "Cliente", Descripcion = "Huésped" }
                );
                await context.SaveChangesAsync();
            }

            // 5. Tipos de Alojamiento
            if (!await context.TiposAlojamiento.AnyAsync())
            {
                context.TiposAlojamiento.AddRange(
                    new TipoAlojamiento { Nombre = "Hotel", Descripcion = "Habitaciones tradicionales" },
                    new TipoAlojamiento { Nombre = "Departamento", Descripcion = "Espacio privado completo" },
                    new TipoAlojamiento { Nombre = "Suite", Descripcion = "Habitación de lujo" },
                    new TipoAlojamiento { Nombre = "Casa", Descripcion = "Vivienda independiente entera" },
                    new TipoAlojamiento { Nombre = "Cabaña", Descripcion = "Alojamiento rústico en naturaleza" },
                    new TipoAlojamiento { Nombre = "Hostal", Descripcion = "Alojamiento económico" }
                );
                await context.SaveChangesAsync();
            }

            // 6. Servicios / Amenities
            if (!await context.Servicios.AnyAsync())
            {
                context.Servicios.AddRange(
                    new Servicio { Nombre = "WiFi", Icono = "wifi" },
                    new Servicio { Nombre = "Piscina", Icono = "pool" },
                    new Servicio { Nombre = "Estacionamiento", Icono = "parking" },
                    new Servicio { Nombre = "Aire Acondicionado", Icono = "ac_unit" },
                    new Servicio { Nombre = "Cocina Equipada", Icono = "kitchen" },
                    new Servicio { Nombre = "TV Cable", Icono = "tv" },
                    new Servicio { Nombre = "Gym", Icono = "fitness_center" },
                    new Servicio { Nombre = "Desayuno Incluido", Icono = "free_breakfast" },
                    new Servicio { Nombre = "Pet Friendly", Icono = "pets" },
                    new Servicio { Nombre = "Lavandería", Icono = "local_laundry_service" }
                );
                await context.SaveChangesAsync();
            }

            // 7. USUARIO ADMIN (admin@hospedaya.com / admin123)
            if (!await context.Usuarios.AnyAsync(u => u.Email == "admin@hospedaya.com"))
            {
                var rolAdmin = await context.Roles.FirstOrDefaultAsync(r => r.Nombre == "Admin");

                var adminUser = new Usuario
                {
                    Email = "admin@hospedaya.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    NombreCompleto = "Administrador HospedaYa",
                    Telefono = "+593000000000",
                    EmailVerificado = true
                };
                context.Usuarios.Add(adminUser);
                await context.SaveChangesAsync();

                if (rolAdmin != null)
                {
                    context.UsuarioRoles.Add(new UsuarioRol
                    {
                        UsuarioId = adminUser.UsuarioId,
                        RolId = rolAdmin.RolId,
                        Estado = true
                    });
                    await context.SaveChangesAsync();
                }
            }

            // 8. ANFITRION DEMO (host@hospedaya.com / host123)
            if (!await context.Usuarios.AnyAsync(u => u.Email == "host@hospedaya.com"))
            {
                var rolAnfitrion = await context.Roles.FirstOrDefaultAsync(r => r.Nombre == "Anfitrion");

                var hostUser = new Usuario
                {
                    Email = "host@hospedaya.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("host123"),
                    NombreCompleto = "Anfitrión Demo",
                    Telefono = "+593111111111",
                    EmailVerificado = true
                };
                context.Usuarios.Add(hostUser);
                await context.SaveChangesAsync();

                if (rolAnfitrion != null)
                {
                    context.UsuarioRoles.Add(new UsuarioRol
                    {
                        UsuarioId = hostUser.UsuarioId,
                        RolId = rolAnfitrion.RolId,
                        Estado = true
                    });
                }

                context.Anfitriones.Add(new Anfitrion
                {
                    UsuarioId = hostUser.UsuarioId,
                    Verificado = true,
                    NombreEmpresa = "HospedaYa Demo Host"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
