using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Aula.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILoteService _loteService;

        public LotesController(ILoteService context)
        {
            _loteService = context;
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var lotes = await _loteService.GetAllLotesAsync(eventoId);

                if(lotes is null)
                    return NoContent();

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar lotes. Erro: {ex.Message}");
            }
        }



        //[HttpPost]
        //public async Task<IActionResult> Post(EventoDto model)
        //{
        //    try
        //    {
        //        var eventos = await _loteService.AddEventos(model);

        //        if (eventos is null)
        //            return BadRequest("Evento não foi criado, houve algum problema.");

        //        return Ok(eventos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar cadastrar um evento. Erro: {ex.Message}");
        //    }
        //}

        [HttpPut("salvar/{eventoId}")]
        public async Task<IActionResult> SaveLotes(int eventoId, [FromBody] LoteDto[] models)
        {
            try
            {
                var lotes = await _loteService.SaveLotes(eventoId, models);

                if (lotes is null)
                    return BadRequest("Evento não foi atualizado, houve algum problema.");

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar lotes. Erro: {ex.Message}");
            }
        }

        [HttpDelete("deletar/{eventoId}/lote/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId)
        {
            try
            {
                if(!await _loteService.DeleteLote(eventoId, loteId))
                    return BadRequest("Lote não foi deletado, houve algum problema. Por favor verificar") ;


                return Ok(new { message = "Lote deleteado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar um lote. Erro: {ex.Message}");
            }
        }
    }
}
