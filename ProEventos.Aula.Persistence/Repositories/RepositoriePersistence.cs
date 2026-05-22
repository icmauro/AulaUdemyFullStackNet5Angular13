using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories
{
    public class RepositoriePersistence : IRepositoriePersistence
    {
        private readonly ProEventoContext _proEventoContext;

        public RepositoriePersistence(ProEventoContext proEventoContext)
        {
            _proEventoContext = proEventoContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _proEventoContext.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _proEventoContext.Set<T>().Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _proEventoContext.Set<T>().Remove(entity);
        }   

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _proEventoContext.Set<T>().RemoveRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _proEventoContext.SaveChangesAsync()) > 0;
        }

        public async Task<T> GetById<T>(Expression<Func<T,bool>> expression) where T : class
        {
            return await _proEventoContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<T> GetById<T>(IQueryable<T> values, Expression<Func<T, bool>> expression) where T : class
        {
            return await values.AsNoTracking().FirstOrDefaultAsync(expression);
        }
    }
}
