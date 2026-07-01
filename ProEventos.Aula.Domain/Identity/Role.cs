using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Domain.Identity
{
    public class Role: IdentityRole<int>
    {
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}
