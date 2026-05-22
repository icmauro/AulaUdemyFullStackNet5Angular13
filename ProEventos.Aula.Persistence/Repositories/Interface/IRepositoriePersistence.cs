using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories.Interface
{
    public interface IRepositoriePersistence
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<T> GetById<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> GetById<T>(IQueryable<T> values, Expression<Func<T, bool>> expression) where T : class;
        Task<bool> SaveChangesAsync();

    }       
}
