using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Domain.Models
{
    public class PalestranteEvento
    {
        public int PalestranteId { get; set; }
        public virtual Palestrante Palestrante { get; set; }
        public int EventoId { get; set; }
        public virtual Evento Evento { get; set; }
    }
}
