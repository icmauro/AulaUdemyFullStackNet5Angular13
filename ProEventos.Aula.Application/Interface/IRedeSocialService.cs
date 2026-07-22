using ProEventos.Aula.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application.Interface
{
    public interface IRedeSocialService
    {
        Task<RedeSocialDto[]> SaveByEvento(int eventoId, RedeSocialDto[] modelsDto);

        Task<bool> DeleteByEvento(int eventoId, int redeSocialId);

        Task<RedeSocialDto[]> SaveByPalestrante(int palestranteId, RedeSocialDto[] modelsDto);

        Task<bool> DeleteBypalestrante(int palestranteId, int redeSocialId);

        Task<RedeSocialDto[]> GetAllByEventoIdAsync(int eventoId);

        Task<RedeSocialDto[]> GetAllByPalestranteIdAsync(int palestranteId);

        Task<RedeSocialDto> GetRedeSocialEventoByIdAsync(int eventoId, int redeSocialId);

        Task<RedeSocialDto> GetRedeSocialPalestranteByIdAsync(int palestranteId, int redeSocialId);
    }
}
