using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories
{
    public class LoteRepositorie : RepositoriePersistence, ILoteRepositorie
    {
        private readonly ProEventoContext _proEventoContext;

        public LoteRepositorie(ProEventoContext proEventoContext) : base(proEventoContext)
        {
            _proEventoContext = proEventoContext;
        }
        public async Task<Lote[]> GetAllLotesAsync(int eventoId)
        {
            IQueryable<Lote> query = _proEventoContext.Lotes;

            query = query.AsNoTracking()
                   .Where(x => x.EventoId == eventoId)
                   .OrderByDescending(l => l.Id);

            return await query.ToArrayAsync();

        }


        public async Task<Lote> GetLoteByIdAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = _proEventoContext.Lotes;

            query = query.AsNoTracking()
                   .Where(x => x.EventoId == eventoId);

            return await GetById<Lote>(query ,x => x.Id == id);
        }
    }
}
