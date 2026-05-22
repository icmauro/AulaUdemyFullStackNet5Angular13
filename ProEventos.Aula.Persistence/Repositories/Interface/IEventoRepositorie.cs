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
        Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false);

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);

        Task<Evento> GetEventosByIdAsync(int id, bool includePalestrantes = false);
    }
}
