using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories.Interface
{
    public interface IPalestranteRepositorie 
    {
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false);

        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);

        Task<Palestrante> GetAllPalestranteByIdAsync(int id, bool includeEventos = false);
    }
}
