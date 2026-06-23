using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Repositories.Interface
{
    public interface ILoteRepositorie : IRepositoriePersistence
    {
        Task<Lote[]> GetAllLotesAsync(int eventoId);

        Task<Lote> GetLoteByIdAsync(int eventoId, int id);
    }
}
