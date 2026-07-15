using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Models;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories
{
    public class EventoRepositorie : RepositoriePersistence, IEventoRepositorie
    {
        private readonly ProEventoContext _proEventoContext;

        public EventoRepositorie(ProEventoContext proEventoContext) : base(proEventoContext)
        {
            _proEventoContext = proEventoContext;
        }
        public async Task<PageList<Evento>> GetAllEventoAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _proEventoContext.Eventos
                                       .Include(e => e.Lotes)
                                       .Include(e => e.RedeSociais);

            if (includePalestrantes)
                query.Include(e => e.Palestrantes);
                
            query = query.AsNoTracking().Where(e => (e.Tema.ToLower().Contains(pageParams.Termos.ToLower()) ||
                                                     e.Local.ToLower().Contains(pageParams.Termos.ToLower()))   &&
                                                      e.UserId == userId)
                                        .OrderBy(e => e.Id);

            return await PageList<Evento>.ToPagedListAsync(query, pageParams.PageNumber, pageParams.PageSize);

        }

        public async Task<Evento> GetEventosByIdAsync(int userId, int id, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _proEventoContext.Eventos
                                       .Include(e => e.Lotes)
                                       .Include(e => e.RedeSociais);

            if (includePalestrantes)
                query.Include(e => e.Palestrantes);

            query.AsNoTracking().Where(e => e.UserId == userId);

            return await GetById<Evento>(query,x => x.Id == id);
        }
    }
}
