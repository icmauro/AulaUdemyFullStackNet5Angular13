using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Models;

namespace ProEventos.Aula.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }
        public DbSet<Evento> Eventos { get; set; }
    }
}
