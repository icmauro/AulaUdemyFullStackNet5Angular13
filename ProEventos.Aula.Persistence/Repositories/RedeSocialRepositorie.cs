using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories
{
    public class RedeSocialRepositorie : RepositoriePersistence, IRedeSocialRepositorie
    {
        private readonly ProEventoContext _proEventoContex;

        public RedeSocialRepositorie(ProEventoContext proEventoContex): base(proEventoContex)
        {
            _proEventoContex = proEventoContex;
        }
        public async Task<RedeSocial[]> GetAllByEventoIdAsync(int eventoId)
        {
            IQueryable<RedeSocial> query = _proEventoContex.RedeSocials;

            query = query.AsNoTracking().Where(e => e.EventoId == eventoId);

            return await query.ToArrayAsync();
        }

        public async Task<RedeSocial[]> GetAllByPalestranteIdAsync(int palestranteId)
        {
            IQueryable<RedeSocial> query = _proEventoContex.RedeSocials;

            query = query.AsNoTracking().Where(e => e.PalestranteId == palestranteId);

            return await query.ToArrayAsync();
        }

        public async Task<RedeSocial> GetRedeSocialEventoByIdAsync(int eventoId, int id)
        {
            IQueryable<RedeSocial> query = _proEventoContex.RedeSocials;

            query = query.AsNoTracking().Where(e => e.EventoId == eventoId && e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<RedeSocial> GetRedeSocialPalestranteByIdAsync(int palestranteId, int id)
        {
            IQueryable<RedeSocial> query = _proEventoContex.RedeSocials;

            query = query.AsNoTracking().Where(e => e.PalestranteId == palestranteId && e.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
