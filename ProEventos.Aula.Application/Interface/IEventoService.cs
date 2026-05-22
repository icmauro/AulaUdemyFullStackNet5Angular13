using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application.Interface
{
    public interface IEventoService
    {
        Task<Evento> AddEventos(Evento model);
        Task<Evento> UpdateEventos(int id, Evento model);
        Task<bool> DeleteEventos(int id);

        Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false);

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);

        Task<Evento> GetEventosByIdAsync(int id, bool includePalestrantes = false);
    }
}
