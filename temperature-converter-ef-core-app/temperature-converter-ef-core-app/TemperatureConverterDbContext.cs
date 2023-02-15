using Microsoft.EntityFrameworkCore;

namespace temperature_converter_ef_core_app
{
    public class TemperatureConverterDbContext : DbContext
    {
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Conversion> Conversions { get; set; }

        private readonly IConfiguration _config;

        public TemperatureConverterDbContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }
    }
}
