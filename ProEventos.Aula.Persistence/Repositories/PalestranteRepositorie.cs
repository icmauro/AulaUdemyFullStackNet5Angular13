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
    public class PalestranteRepositorie : RepositoriePersistence, IPalestranteRepositorie
    {
        private readonly ProEventoContext _proEventoContext;
        public PalestranteRepositorie(ProEventoContext proEventoContext) : base(proEventoContext)
        {
            _proEventoContext = proEventoContext;
        }
        public async Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proEventoContext.Palestrante
                                               .Include(e => e.User)
                                               .Include(e => e.RedeSociais);

            if (includeEventos)
                query.Include(e => e.Eventos);

            query = query.AsNoTracking().Where(e => (e.MiniCurriculo.ToLower().Contains(pageParams.Termos.ToLower()) ||
                                                     e.User.PrimeiroNome.ToLower().Contains(pageParams.Termos.ToLower()) ||
                                                     e.User.UltimoNome.ToLower().Contains(pageParams.Termos.ToLower())) &&
                                                     e.User.Funcao == Domain.Enum.Funcao.Palestrante
                                                    )
                                        .OrderBy(e => e.Id);

            return await PageList<Palestrante>.ToPagedListAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        //public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        //{
        //    IQueryable<Palestrante> query = _proEventoContext.Palestrante
        //                               .Include(e => e.RedeSociais);

        //    if (includeEventos)
        //        query.Include(e => e.Eventos);

        //    query = query.AsNoTracking().Where(e => e.User.PrimeiroNome.ToLower().Contains(nome.ToLower()) &&
        //                                            e.User.UltimoNome.ToLower().Contains(nome.ToLower()))
        //                                .OrderBy(e => e.Id);


        //    return await query.ToArrayAsync();
        //}

        public async Task<Palestrante> GetAllPalestranteByUserIdAsync(int userId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proEventoContext.Palestrante
                                        .Include(e => e.User)
                                        .Include(e => e.RedeSociais);

            if (includeEventos)
                query.Include(e => e.Eventos);


            return await GetById<Palestrante>(query, x => x.UserId == userId);
        }
    }
}
