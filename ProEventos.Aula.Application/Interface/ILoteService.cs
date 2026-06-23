using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application.Interface
{
    public interface ILoteService
    {
        Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] loteDtos);
        Task<bool> DeleteLote(int eventoId, int id);
        Task<LoteDto[]> GetAllLotesAsync(int eventoId);
        Task<LoteDto> GetLoteByIdAsync(int eventoId, int id);
    }
}
