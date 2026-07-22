using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Aula.Application;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Extensions;
using System;
using System.Threading.Tasks;

namespace ProEventos.Aula.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RedeSocialController : ControllerBase
    {
        private readonly IRedeSocialService _redeSocialService;
        private readonly IEventoService _eventoService;
        private readonly IPalestranteService _palestranteService;

        public RedeSocialController(IRedeSocialService redeSocialService, 
                                    IEventoService eventoService, 
                                    IPalestranteService palestranteService)
        {
            _redeSocialService = redeSocialService;
            _eventoService = eventoService;
            _palestranteService = palestranteService;
        }

        [HttpGet("evento/{eventoId}")]
        public async Task<IActionResult> GetByEvento(int eventoId)
        {
            try
            {
                if (!await AutorEvento(eventoId))
                    return Unauthorized("Usuário não autorizado para acessar este evento.");

                var redeSocial = await _redeSocialService.GetAllByEventoIdAsync(eventoId);

                if (redeSocial is null)
                    return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar redes sociais. Erro: {ex.Message}");
            }
        }

        [HttpGet("palestrante")]
        public async Task<IActionResult> GetByPalestrante()
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestrantesByUserIdAsync(User.GetUserId());

                if(palestrante is null)
                    return Unauthorized("Usuário não autorizado para acessar este palestrante.");

                var redeSocial = await _redeSocialService.GetAllByPalestranteIdAsync(palestrante.Id);

                if (redeSocial is null)
                    return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar redes sociais. Erro: {ex.Message}");
            }
        }


        [HttpPut("evento/salvar/{eventoId}")]
        public async Task<IActionResult> SaveByEvento(int eventoId, [FromBody] RedeSocialDto[] models)
        {
            try
            {
                if (!await AutorEvento(eventoId))
                    return Unauthorized("Usuário não autorizado para acessar este evento.");

                var redeSociais = await _redeSocialService.SaveByEvento(eventoId, models);

                if (redeSociais is null)
                    return BadRequest("Rede Social não foi atualizado, houve algum problema.");

                return Ok(redeSociais);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar rede social. Erro: {ex.Message}");
            }
        }

        [HttpPut("palestrante/salvar")]
        public async Task<IActionResult> SaveByPalestrante([FromBody] RedeSocialDto[] models)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestrantesByUserIdAsync(User.GetUserId());

                if (palestrante is null)
                    return Unauthorized("Usuário não autorizado para acessar este palestrante.");

                var redeSociais = await _redeSocialService.SaveByPalestrante(palestrante.Id, models);

                if (redeSociais is null)
                    return BadRequest("Rede Social não foi atualizado, houve algum problema.");

                return Ok(redeSociais);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar rede social. Erro: {ex.Message}");
            }
        }

        [HttpDelete("deletar/evento/{eventoId}/{redeSocialId}")]
        public async Task<IActionResult> DeleteByEvento(int eventoId, int redeSocialId)
        {
            try
            {
                if (!await AutorEvento(eventoId))
                    return Unauthorized("Usuário não autorizado para acessar este evento.");

                var redeSocial = await _redeSocialService.GetRedeSocialEventoByIdAsync(eventoId, redeSocialId);

                if(redeSocial is null)
                    return NoContent();

                if (!await _redeSocialService.DeleteByEvento(eventoId, redeSocialId))
                    return BadRequest("Rede Social não foi deletado, houve algum problema. Por favor verificar");


                return Ok(new { message = "Rede Social deleteado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar um lote. Erro: {ex.Message}");
            }
        }

        [HttpDelete("deletar/palestrante/{redeSocialId}")]
        public async Task<IActionResult> DeleteByPalestrante(int redeSocialId)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestrantesByUserIdAsync(User.GetUserId());

                if (palestrante is null)
                    return Unauthorized("Usuário não autorizado para acessar este palestrante.");

                var redeSocial = await _redeSocialService.GetRedeSocialPalestranteByIdAsync(palestrante.Id, redeSocialId);

                if (redeSocial is null)
                    return NoContent();

                if (!await _redeSocialService.DeleteBypalestrante(palestrante.Id, redeSocialId))
                    return BadRequest("Rede Social não foi deletado, houve algum problema. Por favor verificar");


                return Ok(new { message = "Rede Social deleteado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar um lote. Erro: {ex.Message}");
            }
        }

        private async Task<bool> AutorEvento(int eventoId)
        { 
            var evento = await _eventoService.GetEventosByIdAsync(User.GetUserId(), eventoId);

            if (evento is null)
                return false;

            return true;
        }
    }
}
