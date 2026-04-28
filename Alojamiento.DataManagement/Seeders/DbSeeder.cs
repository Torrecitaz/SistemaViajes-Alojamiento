using System.Linq;
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
                    new Moneda { Nombre = "Euro", Codigo = "EUR", Simbolo = "€" }
                );
                await context.SaveChangesAsync();
            }

            // 2. Países
            if (!await context.Paises.AnyAsync())
            {
                context.Paises.Add(new Pais { Nombre = "Ecuador", CodigoISO2 = "EC", CodigoISO3 = "ECU" });
                await context.SaveChangesAsync();
            }

            // 3. Ciudades
            if (!await context.Ciudades.AnyAsync())
            {
                var ecuador = await context.Paises.FirstOrDefaultAsync(p => p.Nombre == "Ecuador");
                if (ecuador != null)
                {
                    context.Ciudades.AddRange(
                        new Ciudad { Nombre = "Quito", PaisId = ecuador.PaisId, EsCapital = true },
                        new Ciudad { Nombre = "Guayaquil", PaisId = ecuador.PaisId, EsCapital = false }
                    );
                    await context.SaveChangesAsync();
                }
            }

            // 4. Roles
            if (!await context.Roles.AnyAsync())
            {
                context.Roles.AddRange(
                    new Rol { Nombre = "Admin", Descripcion = "Administrador Global" },
                    new Rol { Nombre = "Cliente", Descripcion = "Huésped" },
                    new Rol { Nombre = "Anfitrion", Descripcion = "Propietario de Alojamientos" }
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
                    new TipoAlojamiento { Nombre = "Casa", Descripcion = "Vivienda independiente entera" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
