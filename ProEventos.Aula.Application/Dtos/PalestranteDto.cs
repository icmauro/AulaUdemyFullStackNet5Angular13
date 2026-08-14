using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application.Dtos
{
    public class PalestranteDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserUpdateDto User { get; set; }
        public string MiniCurriculo { get; set; }
        public virtual IEnumerable<RedeSocialDto> RedeSociais { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<EventoDto> Eventos { get; set; }
    }
}
