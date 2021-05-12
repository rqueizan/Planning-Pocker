using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth.Model;

namespace Planning.Pocker.Api.NoAuth.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Carta> Carta { get; set; }

        public DbSet<Historial> Historial { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Voto> Voto { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    }
}
