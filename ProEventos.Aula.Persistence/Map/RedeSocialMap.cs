using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Map
{
    public class RedeSocialMap: IEntityTypeConfiguration<RedeSocial>
    {
        public void Configure(EntityTypeBuilder<RedeSocial> builder)
        {
            builder.HasKey(rs => rs.Id);
            builder.Property(rs => rs.Nome).IsRequired().HasMaxLength(250);
            builder.Property(rs => rs.Url).IsRequired().HasMaxLength(250);
        }
    }
}
