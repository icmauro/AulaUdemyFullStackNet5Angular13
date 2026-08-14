using ProEventos.Aula.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application.Interface
{
    public interface IPalestranteService
    {
        Task<PalestranteDto> AddPalestrante(int userId);
        Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto model);
        Task<PageListDto<PalestranteDto>> GetAllPalestranteAsync(PageParamsDto pageParams, bool includeEventos = false);
        Task<PalestranteDto> GetPalestrantesByUserIdAsync(int userId, bool includePalestrantes = false);
    }
}
