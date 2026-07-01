using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Domain.Identity;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories
{
    public class UserRepositorie : RepositoriePersistence, IUserRepositorie
    {
        private readonly ProEventoContext _proEventoContext;
        public UserRepositorie(ProEventoContext proEventoContext ) : base(proEventoContext)
        {
            _proEventoContext = proEventoContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _proEventoContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
           return await _proEventoContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByNomeAsync(string nome)
        {
            return await _proEventoContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == nome.ToLower());
                                                                                        
        }

    }
}
