using Microsoft.EntityFrameworkCore;

namespace DemoAspNet
{
    public class DemoAspNetDbContext:DbContext
    {
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string database_name = "FruitsDB";
            dbContextOptionsBuilder.UseSqlServer($"{connection_string};Database={database_name};");
        }
    }
}