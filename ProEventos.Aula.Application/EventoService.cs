using AutoMapper;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Domain.Models;
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
        public async Task<EventoDto> AddEventos(EventoDto modelDto)
        {
            var model = _mapper.Map<Evento>(modelDto);

            _eventoRepositorie.Add<Evento>(model);

            return await SaveEvento(_eventoRepositorie, model);

        }

        public async Task<EventoDto> UpdateEventos(int id, EventoDto modelDto)
        {
            var model = _mapper.Map<Evento>(modelDto);

            var evento = await  _eventoRepositorie.GetEventosByIdAsync(id);

            if (evento is null)
                return null;

            model.Id = evento.Id;

            _eventoRepositorie.Update<Evento>(model);

            return await SaveEvento(_eventoRepositorie, model);
        }

        public async Task<bool> DeleteEventos(int id)
        {
            var evento = await _eventoRepositorie.GetEventosByIdAsync(id);
            var sucesso = false;

            if (evento is null)
                return false;

            _eventoRepositorie.Delete<Evento>(evento);

            sucesso = await SaveEvento(_eventoRepositorie, evento) != null;

            return sucesso;
        }

        public async Task<EventoDto[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            var resultado = await _eventoRepositorie.GetAllEventoAsync(includePalestrantes);

            return _mapper.Map<EventoDto[]>(resultado);

        }

        public async Task<EventoDto> GetEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            var resultado = await _eventoRepositorie.GetEventosByIdAsync(id, includePalestrantes);
            
            return _mapper.Map<EventoDto>(resultado);
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var resultado = await _eventoRepositorie.GetAllEventosByTemaAsync(tema, includePalestrantes);

            return _mapper.Map<EventoDto[]>(resultado);
        }

        private async Task<EventoDto> SaveEvento(IEventoRepositorie eventoRepositorie, Evento model)
        {
            Evento resultado;

            if (await eventoRepositorie.SaveChangesAsync())
            {
                resultado = await eventoRepositorie.GetEventosByIdAsync(model.Id);
                return _mapper.Map<EventoDto>(resultado);
            }

            return null;
        }
    }
}
