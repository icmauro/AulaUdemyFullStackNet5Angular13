using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories.Interface
{
    public interface IEventoRepositorie : IRepositoriePersistence
    {
        Task<Evento[]> GetAllEventoAsync(int userId, bool includePalestrantes = false);

        Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false);

        Task<Evento> GetEventosByIdAsync(int userId, int id, bool includePalestrantes = false);
    }
}
