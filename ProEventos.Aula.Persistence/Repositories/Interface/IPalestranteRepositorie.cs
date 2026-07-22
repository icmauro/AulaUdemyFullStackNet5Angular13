using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories.Interface
{
    public interface IPalestranteRepositorie : IRepositoriePersistence
    {
        Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false);

        Task<Palestrante> GetAllPalestranteByUserIdAsync(int userId, bool includeEventos = false);
    }
}
