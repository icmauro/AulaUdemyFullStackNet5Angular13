using Microsoft.AspNetCore.Identity;
using ProEventos.Aula.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories.Interface
{
    public interface IUserRepositorie : IRepositoriePersistence
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByNomeAsync(string nome);

    }
}
