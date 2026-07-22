using AutoMapper;
using Microsoft.Extensions.Logging;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence.Repositories;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application
{
    public class RedeSocialService : IRedeSocialService
    {
        private readonly IRedeSocialRepositorie _redeSocialRepositorie;
        private readonly IMapper _mapper;
        public RedeSocialService(IRedeSocialRepositorie redeSocialRepositorie,
                                 IMapper mapper)
        {
            _redeSocialRepositorie = redeSocialRepositorie;
            _mapper = mapper;
        }

        public async Task<RedeSocialDto[]> SaveByEvento(int eventoId, RedeSocialDto[] modelsDto)
        {
            var eventos = await _redeSocialRepositorie.GetAllByEventoIdAsync(eventoId);

            foreach (var model in modelsDto)
            {
                if (model.Id > 0)
                {
                    var redeSocial = eventos.FirstOrDefault(x => x.Id == model.Id);
                    model.EventoId = eventoId;

                    _mapper.Map(model, redeSocial);

                    _redeSocialRepositorie.Update<RedeSocial>(redeSocial);

                }
                else
                {
                    await AddRedeSocial(eventoId, model, true);
                }
            }

            return await Save(_redeSocialRepositorie, eventoId, true);
        }

        public async Task<RedeSocialDto[]> SaveByPalestrante(int palestranteId, RedeSocialDto[] modelsDto)
        {
            var palestrantes = await _redeSocialRepositorie.GetAllByPalestranteIdAsync(palestranteId);

            foreach (var model in modelsDto)
            {
                if (model.Id > 0)
                {
                    var redeSocial = palestrantes.FirstOrDefault(x => x.Id == model.Id);
                    model.PalestranteId = palestranteId;

                    _mapper.Map(model, redeSocial);

                    _redeSocialRepositorie.Update<RedeSocial>(redeSocial);

                }
                else
                {
                    await AddRedeSocial(palestranteId, model , false);
                }
            }

            return await Save(_redeSocialRepositorie, palestranteId, false);
        }
        public async Task<bool> DeleteByEvento(int eventoId, int redeSocialId)
        {
            var redeSocial = await _redeSocialRepositorie.GetRedeSocialEventoByIdAsync(eventoId, redeSocialId);
            var sucesso = false;

            if (redeSocial is null)
                return sucesso;

            _redeSocialRepositorie.Delete<RedeSocial>(redeSocial);

            sucesso = await _redeSocialRepositorie.SaveChangesAsync();

            return sucesso;
        }

        public async Task<bool> DeleteBypalestrante(int palestranteId, int redeSocialId)
        {
            var redeSocial = await _redeSocialRepositorie.GetRedeSocialPalestranteByIdAsync(palestranteId, redeSocialId);
            var sucesso = false;

            if (redeSocial is null)
                return sucesso;

            _redeSocialRepositorie.Delete<RedeSocial>(redeSocial);

            sucesso = await _redeSocialRepositorie.SaveChangesAsync();

            return sucesso;
        }

        public async Task<RedeSocialDto[]> GetAllByEventoIdAsync(int eventoId)
        {
            var resultado = await _redeSocialRepositorie.GetAllByEventoIdAsync(eventoId);

            return _mapper.Map<RedeSocialDto[]>(resultado);
        }

        public async Task<RedeSocialDto[]> GetAllByPalestranteIdAsync(int palestranteId)
        {
            var resultado = await _redeSocialRepositorie.GetAllByPalestranteIdAsync(palestranteId);

            return _mapper.Map<RedeSocialDto[]>(resultado);
        }

        public async Task<RedeSocialDto> GetRedeSocialEventoByIdAsync(int eventoId, int redeSocialId)
        {
            var resultado = await _redeSocialRepositorie.GetRedeSocialEventoByIdAsync(eventoId, redeSocialId);

            return _mapper.Map<RedeSocialDto>(resultado);
        }

        public async Task<RedeSocialDto> GetRedeSocialPalestranteByIdAsync(int palestranteId, int redeSocialId)
        {
            var resultado = await _redeSocialRepositorie.GetRedeSocialPalestranteByIdAsync(palestranteId, redeSocialId);

            return _mapper.Map<RedeSocialDto>(resultado);
        }

        private async Task AddRedeSocial(int id, RedeSocialDto redeSocialDto, bool isEvento)
        {
            var model = _mapper.Map<RedeSocial>(redeSocialDto);

            if (isEvento)
            {
                model.EventoId = id;
                model.PalestranteId = null;
            }
            else
            {
                model.PalestranteId = id;
                model.EventoId = null;
            }


            _redeSocialRepositorie.Add<RedeSocial>(model);
        }

        private async Task<RedeSocialDto[]> Save(IRedeSocialRepositorie redeSocialRepositorie,
                                                 int Id,
                                                 bool isEvento)
        {
            RedeSocial[] resultado;

            if (await redeSocialRepositorie.SaveChangesAsync())
            {
                if(isEvento)
                  resultado = await redeSocialRepositorie.GetAllByEventoIdAsync(Id);
                else
                  resultado = await redeSocialRepositorie.GetAllByPalestranteIdAsync(Id);

                return _mapper.Map<RedeSocialDto[]>(resultado);
            }

            return null;
        }
    }
}
