using Microsoft.EntityFrameworkCore;

namespace WinFormsApp2
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Userr;Username=postgres;Password=2502;");
        }
    }
}