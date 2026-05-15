using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Domain.Models
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public int? EventoId { get; set; }
        public virtual Evento Evento { get; set; }
        public int? PalestranteId { get; set; }
        public virtual Palestrante Palestrante { get; set; }
    }
}
