using Microsoft.AspNetCore.Identity;
using ProEventos.Aula.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Domain.Identity
{
    public class User: IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public Titulo Titulo { get; set; }
        public string Descricao { get; set; }
        public Funcao Funcao { get; set; }
        public string ImagemURL { get; set; }
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}
