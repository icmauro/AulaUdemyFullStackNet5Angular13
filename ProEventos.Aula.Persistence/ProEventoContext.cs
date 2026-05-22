using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Domain.Models;

namespace ProEventos.Aula.Persistence
{
    public class ProEventoContext: DbContext
    {
        public ProEventoContext(DbContextOptions<ProEventoContext> options) : base(options) 
        {
            
        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrante { get; set; }
        public DbSet<RedeSocial> RedeSocials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProEventoContext).Assembly);
           
           base.OnModelCreating(modelBuilder);
        }

    }
}
