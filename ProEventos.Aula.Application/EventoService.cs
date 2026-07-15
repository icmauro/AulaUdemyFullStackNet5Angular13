using AutoMapper;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Models;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepositorie _eventoRepositorie;
        private readonly IMapper _mapper;

        public EventoService(IEventoRepositorie eventoRepositorie,
                             IMapper mapper)
        {
            _eventoRepositorie = eventoRepositorie;
            _mapper = mapper;
        }
        public async Task<EventoDto> AddEventos(int userId, EventoDto modelDto)
        {
            var model = _mapper.Map<Evento>(modelDto);

            model.UserId = userId;

            _eventoRepositorie.Add<Evento>(model);

            return await Save(userId, _eventoRepositorie, model);

        }

        public async Task<EventoDto> UpdateEventos(int userId, int id, EventoDto modelDto)
        {
            var model = _mapper.Map<Evento>(modelDto);

            model.UserId = userId;

            var evento = await  _eventoRepositorie.GetEventosByIdAsync(userId, id);

            if (evento is null)
                return null;

            model.Id = evento.Id;

            _eventoRepositorie.Update<Evento>(model);

            return await Save(userId, _eventoRepositorie, model);
        }

        public async Task<bool> DeleteEventos(int userId, int id)
        {
            var evento = await _eventoRepositorie.GetEventosByIdAsync(userId, id);
            var sucesso = false;

            if (evento is null)
                return sucesso;

            _eventoRepositorie.Delete<Evento>(evento);

            sucesso = await Save(userId, _eventoRepositorie, evento, true) != null;

            return sucesso;
        }

        public async Task<PageListDto<EventoDto>> GetAllEventoAsync(int userId, PageParamsDto pageParamsDto, bool includePalestrantes = false)
        {
            var pageParams = _mapper.Map<PageParams>(pageParamsDto);

            var resultado = await _eventoRepositorie.GetAllEventoAsync(userId, pageParams, includePalestrantes);

            var eventosDTO = _mapper.Map<List<EventoDto>>(resultado);

            return new PageListDto<EventoDto>(eventosDTO, resultado.TotalCount, resultado.CurrentPage, resultado.PageSize);

        }

        public async Task<EventoDto> GetEventosByIdAsync(int userId, int id, bool includePalestrantes = false)
        {
            var resultado = await _eventoRepositorie.GetEventosByIdAsync(userId, id, includePalestrantes);
            
            return _mapper.Map<EventoDto>(resultado);
        }

        private async Task<EventoDto> Save(int userId, IEventoRepositorie eventoRepositorie, Evento model, bool delete = false)
        {
            Evento resultado;

            if (await eventoRepositorie.SaveChangesAsync())
            {
                if (delete)
                   return new EventoDto();

                resultado = await eventoRepositorie.GetEventosByIdAsync(userId, model.Id);
                return _mapper.Map<EventoDto>(resultado);
            }

            return null;
        }
    }
}
