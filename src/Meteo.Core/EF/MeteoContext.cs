using Meteo.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Meteo.Core.EF
{
    public class MeteoContext : DbContext
    {
        private readonly SqlOptions _options;
        public DbSet<City> Cities { get; set; }

        public MeteoContext(DbContextOptions<MeteoContext> context,
            IOptions<SqlOptions> options) : base(context)
        {
            _options = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase("Meteo");

                return;
            }
            optionsBuilder.UseSqlServer(_options.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasKey(x => x.Id);
        }
    }
}