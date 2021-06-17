using Microsoft.EntityFrameworkCore;

namespace temperature_converter_app_ef_core.Models
{
    public class TemperatureConverterDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Conversion> Conversions { get; set; }

        public TemperatureConverterDbContext(DbContextOptions<TemperatureConverterDbContext> options) : base(options) { }
    }
}
