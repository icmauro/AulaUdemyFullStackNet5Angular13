using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories.Interface
{
    public interface IEventoRepositorie : IRepositoriePersistence
    {
        Task<PageList<Evento>> GetAllEventoAsync(int userId, PageParams pageParams, bool includePalestrantes = false);

      //Task<PageList<Evento>> GetAllEventosByTemaAsync(int userId, PageParams pageParams, string tema, bool includePalestrantes = false);

        Task<Evento> GetEventosByIdAsync(int userId, int id, bool includePalestrantes = false);
    }
}
