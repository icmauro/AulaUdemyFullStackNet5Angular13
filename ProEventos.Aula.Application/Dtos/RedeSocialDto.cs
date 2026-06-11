using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application.Dtos
{
    public class RedeSocialDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public int? EventoId { get; set; }
        public virtual EventoDto Evento { get; set; }
        public int? PalestranteId { get; set; }
        public virtual PalestranteDto Palestrante { get; set; }
    }
}
