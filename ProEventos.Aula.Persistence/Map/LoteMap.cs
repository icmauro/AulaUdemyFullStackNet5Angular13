using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Map
{
    public class LoteMap : IEntityTypeConfiguration<Lote>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Lote> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Nome).IsRequired().HasMaxLength(250);
            builder.Property(l => l.Preco).IsRequired();
            builder.Property(l => l.Qtd).IsRequired();
            builder.Property(l => l.DataInicio).IsRequired();
            builder.Property(l => l.DataFim).IsRequired();
        }
    }
}
