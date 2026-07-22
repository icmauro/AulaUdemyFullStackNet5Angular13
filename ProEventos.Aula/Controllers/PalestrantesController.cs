using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Aula.Application;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Extensions;
using System;
using System.Threading.Tasks;

namespace ProEventos.Aula.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PalestrantesController : ControllerBase
    {
        private readonly IPalestranteService _palestranteService;
        public PalestrantesController(IPalestranteService palestranteService)
        {
            _palestranteService = palestranteService;
        }

        [HttpGet("Todos")]
        public async Task<IActionResult> Get([FromQuery] PageParamsDto pageParamsDto)
        {
            try
            {
                var palestrantes = await _palestranteService.GetAllPalestranteAsync(pageParamsDto);

                Response.AddPagination(palestrantes.CurrentPage, palestrantes.PageSize, palestrantes.TotalCount, palestrantes.TotalPages);

                if (palestrantes is null)
                    return NoContent();

                return Ok(palestrantes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar os palestrantes. Erro: {ex.Message}");
            }
        }

        [HttpGet("Buscar")]
        public async Task<IActionResult> GetPalestrante()
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestrantesByUserIdAsync(User.GetUserId());

                if (palestrante is null)
                    return NoContent();

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar palestrante. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PalestranteAddDto model)
        {
            try
            {
                var palestrante = await _palestranteService.AddPalestrante(User.GetUserId(), model);

                if (palestrante is null)
                    return BadRequest("Palestrante não foi criado, houve algum problema.");

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar cadastrar um palestrante. Erro: {ex.Message}");
            }
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Put(PalestranteUpdateDto model)
        {
            try
            {
                var palestrante = await _palestranteService.UpdatePalestrante(User.GetUserId(), model);

                if (palestrante is null)
                    return BadRequest("Palestrante não foi atualizado, houve algum problema.");

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar um evento. Erro: {ex.Message}");
            }
        }

    }
}
