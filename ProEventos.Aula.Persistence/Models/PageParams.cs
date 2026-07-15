using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Models
{
    public class PageParams
    {
        public const int MaxPageSize = 30;

        public int PageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;

        public int PageSize 
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string Termos { get; set; }
    }
}
