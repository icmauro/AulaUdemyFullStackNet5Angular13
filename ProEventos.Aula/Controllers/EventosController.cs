using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService context)
        {
            _eventoService = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventoAsync();

                if(eventos is null)
                    return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var eventos = await _eventoService.GetEventosByIdAsync(id);

                if (eventos is null)
                    return NotFound("Eventos por Id não encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema);

                if (eventos is null)
                    return NotFound("Eventos por tema não encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var eventos = await _eventoService.AddEventos(model);

                if (eventos is null)
                    return BadRequest("Evento não foi criado, houve algum problema.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar cadastrar um evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> Put(int id,Evento model)
        {
            try
            {
                var eventos = await _eventoService.UpdateEventos(id,model);

                if (eventos is null)
                    return BadRequest("Evento não foi atualizado, houve algum problema.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar um evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(!await _eventoService.DeleteEventos(id))
                    return BadRequest("Evento não foi deletado, houve algum problema.") ;


                return Ok("Evento deleteado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar um evento. Erro: {ex.Message}");
            }
        }
    }
}
