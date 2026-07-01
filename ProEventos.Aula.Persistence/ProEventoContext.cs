using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Domain.Identity;
using ProEventos.Aula.Domain.Models;

namespace ProEventos.Aula.Persistence
{
    public class ProEventoContext: IdentityDbContext<User, Role, int, 
                                                     IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                                     IdentityRoleClaim<int>, IdentityUserToken<int>>
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProEventoContext).Assembly);
           
          
        }

    }
}
