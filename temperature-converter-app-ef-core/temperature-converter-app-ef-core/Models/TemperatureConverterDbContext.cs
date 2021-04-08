using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace temperature_converter_app_ef_core.Models
{
    public class TemperatureConverterDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Conversion> Conversions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=TemperatureConverterDb;Trusted_Connection=True;");
        }
    }
}
