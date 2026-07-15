using Microsoft.EntityFrameworkCore;

namespace Servidor.Contenido
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Plato> Platos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
