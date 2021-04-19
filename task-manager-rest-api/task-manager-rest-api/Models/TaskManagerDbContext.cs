using Microsoft.EntityFrameworkCore;

namespace task_manager_rest_api.Models
{
    public class TaskManagerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=TaskManagerApiRestDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Priority>().HasData(
                new Priority() { PriorityId = 1, Title = "High" },
                new Priority() { PriorityId = 2, Title = "Medium" },
                new Priority() { PriorityId = 3, Title = "Low" }
            );
        }
    }
}