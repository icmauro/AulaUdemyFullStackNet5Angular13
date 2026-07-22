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
    public class LoteService : ILoteService
    {
        private readonly ILoteRepositorie _loteRepositorie;
        private readonly IMapper _mapper;

        public LoteService(ILoteRepositorie loteRepositorie,
                           IMapper mapper)
        {
            _loteRepositorie = loteRepositorie;
            _mapper = mapper;
        }
        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] lotesDto)
        {

            var lotes = await _loteRepositorie.GetAllLotesAsync(eventoId);

            foreach (var model in lotesDto)
            {
                if (model.Id > 0)
                {
                    var lote = lotes.FirstOrDefault(x => x.Id == model.Id);
                    model.EventoId = eventoId;

                    _mapper.Map(model, lote);

                    _loteRepositorie.Update<Lote>(lote);

                }
                else 
                {
                   await AddLote(eventoId, model);
                }
            }

            return await Save(_loteRepositorie, eventoId);

        }

        public async Task AddLote(int eventoId, LoteDto loteDto)
        {
            var model = _mapper.Map<Lote>(loteDto);

            model.EventoId = eventoId;

            _loteRepositorie.Add<Lote>(model);
        }

        public async Task<bool> DeleteLote(int eventoId, int id)
        {
            var lote = await _loteRepositorie.GetLoteByIdAsync(eventoId, id);
            var sucesso = false;

            if (lote is null)
                return sucesso;

            _loteRepositorie.Delete<Lote>(lote);

            sucesso = await _loteRepositorie.SaveChangesAsync();

            return sucesso;
        }

        public async Task<LoteDto[]> GetAllLotesAsync(int eventoId)
        {
            var resultado = await _loteRepositorie.GetAllLotesAsync(eventoId);

            return _mapper.Map<LoteDto[]>(resultado);

        }

        public async Task<LoteDto> GetLoteByIdAsync(int eventoId, int id)
        {
            var resultado = await _loteRepositorie.GetLoteByIdAsync(eventoId, id);
            
            return _mapper.Map<LoteDto>(resultado);
        }


        private async Task<LoteDto[]> Save(ILoteRepositorie loteRepositorie, 
                                           int eventoId)
        {

            if (await loteRepositorie.SaveChangesAsync())
            {
               var resultado = await loteRepositorie.GetAllLotesAsync(eventoId);
                return _mapper.Map<LoteDto[]>(resultado);
            }

            return null;
        }
    }
}
