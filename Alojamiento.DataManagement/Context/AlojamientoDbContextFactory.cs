using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Alojamiento.DataManagement.Context
{
    public class AlojamientoDbContextFactory : IDesignTimeDbContextFactory<AlojamientoDbContext>
    {
        public AlojamientoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AlojamientoDbContext>();
            optionsBuilder.UseNpgsql("Host=db.lirydorpdbvnrfoijyru.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=mateo0984145614");

            return new AlojamientoDbContext(optionsBuilder.Options);
        }
    }
}
