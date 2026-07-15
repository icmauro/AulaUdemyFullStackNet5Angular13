using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application.Interface
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(int userId, EventoDto  model);
        Task<EventoDto> UpdateEventos(int userId, int id, EventoDto model);
        Task<bool> DeleteEventos(int userId, int id);
        Task<PageListDto<EventoDto>> GetAllEventoAsync(int userId, PageParamsDto pageParams, bool includePalestrantes = false);
        Task<EventoDto> GetEventosByIdAsync(int userId, int id, bool includePalestrantes = false);
    }
}
