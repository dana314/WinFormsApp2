using Microsoft.EntityFrameworkCore;

namespace WinFormsApp2
{
    /// <summary>
    /// Класс для работы с PstgreSQL
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Пользователи в бд
        /// </summary>
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Userr;Username=postgres;Password=2502;");
        }
    }
}