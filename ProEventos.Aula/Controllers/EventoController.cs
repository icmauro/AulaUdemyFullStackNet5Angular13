using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Aula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Aula.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        public EventoController()
        {
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return new List<Evento>();
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return new List<Evento>();
        }

        [HttpPost]
        public string Post()
        {
            return "exemplo de POST";
        }
    }
}
