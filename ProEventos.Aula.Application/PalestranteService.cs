using AutoMapper;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Models;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application
{
    public class PalestranteService: IPalestranteService
    {
        private readonly IPalestranteRepositorie _palestranteRepositorie;
        private readonly IMapper _mapper;

        public PalestranteService(IPalestranteRepositorie palestranteRepositorie,
                                  IMapper mapper)
        {
            _palestranteRepositorie = palestranteRepositorie;
            _mapper = mapper;
        }
        public async Task<PalestranteDto> AddPalestrante(int userId, PalestranteAddDto modelDto)
        {
           var user = await GetPalestrantesByUserIdAsync(userId);

            if (user is not null)
                return user;

            var model = _mapper.Map<Palestrante>(modelDto);

            model.UserId = userId;

            _palestranteRepositorie.Add<Palestrante>(model);

            return await Save(userId, _palestranteRepositorie, model);

        }

        public async Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto modelDto)
        {
            var model = _mapper.Map<Palestrante>(modelDto);

            model.UserId = userId;

            var palestrante = await _palestranteRepositorie.GetAllPalestranteByUserIdAsync(userId);

            if (palestrante is null)
                return null;

            model.Id = palestrante.Id;

            _palestranteRepositorie.Update<Palestrante>(model);

            return await Save(userId, _palestranteRepositorie, model);
        }

        public async Task<PageListDto<PalestranteDto>> GetAllPalestranteAsync(PageParamsDto pageParamsDto, bool includeEventos = false)
        {
            var pageParams = _mapper.Map<PageParams>(pageParamsDto);

            var resultado = await _palestranteRepositorie.GetAllPalestrantesAsync(pageParams, includeEventos);

            var palestranteDTO = _mapper.Map<List<PalestranteDto>>(resultado);

            return new PageListDto<PalestranteDto>(palestranteDTO, resultado.TotalCount, resultado.CurrentPage, resultado.PageSize);

        }

        public async Task<PalestranteDto> GetPalestrantesByUserIdAsync(int userId, bool includeEventoss = false)
        {
            var resultado = await _palestranteRepositorie.GetAllPalestranteByUserIdAsync(userId, includeEventoss);

            return _mapper.Map<PalestranteDto>(resultado);
        }

        private async Task<PalestranteDto> Save(int userId, IPalestranteRepositorie palestranteRepositorie, Palestrante model, bool delete = false)
        {
            Palestrante resultado;

            if (await palestranteRepositorie.SaveChangesAsync())
            {
                if (delete)
                    return new PalestranteDto();

                resultado = await palestranteRepositorie.GetAllPalestranteByUserIdAsync(userId);
                return _mapper.Map<PalestranteDto>(resultado);
            }

            return null;
        }
    }
}
