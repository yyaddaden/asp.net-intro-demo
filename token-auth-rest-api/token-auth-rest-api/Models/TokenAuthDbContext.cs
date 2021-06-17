using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace token_auth_rest_api.Models
{
    public class TokenAuthDbContext : IdentityDbContext<ApiUser>
    {
        public DbSet<ApiUser> ApiUsers { get; set; }

        public TokenAuthDbContext(DbContextOptions<TokenAuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}