using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Domain.Models
{
    public class Palestrante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemUrl { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<RedeSocial> RedeSociais { get; set; }
        public virtual IEnumerable<PalestranteEvento> PalestranteEventos { get; set; }
    }
}
