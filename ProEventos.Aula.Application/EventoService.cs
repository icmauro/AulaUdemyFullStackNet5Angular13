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

        public EventoService(IEventoRepositorie eventoRepositorie)
        {
            _eventoRepositorie = eventoRepositorie;
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            _eventoRepositorie.Add<Evento>(model);

            return await SaveEvento(_eventoRepositorie, model);

        }

        public async Task<Evento> UpdateEventos(int id, Evento model)
        {
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

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {

            return await _eventoRepositorie.GetAllEventoAsync(includePalestrantes);

        }

        public async Task<Evento> GetEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            return await _eventoRepositorie.GetEventosByIdAsync(id, includePalestrantes);
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            return await _eventoRepositorie.GetAllEventosByTemaAsync(tema, includePalestrantes);
        }

        private async Task<Evento> SaveEvento(IEventoRepositorie eventoRepositorie, Evento model)
        {
            if (await eventoRepositorie.SaveChangesAsync())
                return await eventoRepositorie.GetEventosByIdAsync(model.Id);

            return null;
        }
    }
}
