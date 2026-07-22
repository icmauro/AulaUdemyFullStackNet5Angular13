using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories.Interface
{
    public interface IRedeSocialRepositorie: IRepositoriePersistence
    {
        Task<RedeSocial> GetRedeSocialEventoByIdAsync(int eventoId, int id);
        Task<RedeSocial> GetRedeSocialPalestranteByIdAsync(int palestranteId, int id);
        Task<RedeSocial[]> GetAllByEventoIdAsync(int eventoId);
        Task<RedeSocial[]> GetAllByPalestranteIdAsync(int palestranteId);
    }
}
