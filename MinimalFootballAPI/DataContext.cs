using Microsoft.EntityFrameworkCore;

namespace MinimalFootballAPI
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=football;Username=postgres;Password=;");
        }

        public DbSet<Clube> Clubes => Set<Clube>();
    }
}
