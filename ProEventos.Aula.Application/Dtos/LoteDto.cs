using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Qtd { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public int EventoId { get; set; }

        [JsonIgnore]
        public virtual EventoDto Evento { get; set; }
    }
}
