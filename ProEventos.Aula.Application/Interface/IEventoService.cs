using ProEventos.Aula.Application.Dtos;
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
        Task<EventoDto> AddEventos(EventoDto  model);
        Task<EventoDto> UpdateEventos(int id, EventoDto model);
        Task<bool> DeleteEventos(int id);

        Task<EventoDto[]> GetAllEventoAsync(bool includePalestrantes = false);

        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);

        Task<EventoDto> GetEventosByIdAsync(int id, bool includePalestrantes = false);
    }
}
