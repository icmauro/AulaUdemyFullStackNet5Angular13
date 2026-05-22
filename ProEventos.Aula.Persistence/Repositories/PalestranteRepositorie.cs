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
    public class PalestranteRepositorie : RepositoriePersistence, IPalestranteRepositorie
    {
        private readonly ProEventoContext _proEventoContext;
        public PalestranteRepositorie(ProEventoContext proEventoContext) : base(proEventoContext)
        {
            _proEventoContext = proEventoContext;
        }
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proEventoContext.Palestrante
                                       .Include(e => e.RedeSociais);

            if (includeEventos)
                query.Include(e => e.Eventos);

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proEventoContext.Palestrante
                                       .Include(e => e.RedeSociais);

            if (includeEventos)
                query.Include(e => e.Eventos);

            query = query.AsNoTracking().Where(e => e.Nome.ToLower().Contains(nome.ToLower()))
                                        .OrderBy(e => e.Id);


            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteByIdAsync(int id, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proEventoContext.Palestrante
                                       .Include(e => e.RedeSociais);

            if (includeEventos)
                query.Include(e => e.Eventos);


            return await GetById<Palestrante>(query, x => x.Id == id);
        }
    }
}
