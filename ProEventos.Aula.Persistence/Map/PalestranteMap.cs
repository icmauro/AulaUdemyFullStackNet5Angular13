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
    public class PalestranteMap: IEntityTypeConfiguration<Palestrante>
    {
        public void Configure(EntityTypeBuilder<Palestrante> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(250);
            builder.Property(p => p.MiniCurriculo).IsRequired();
            //builder.Property(p => p.ImagemUrl).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Telefone).HasMaxLength(20);
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.HasMany(p => p.RedeSociais)
                .WithOne(rs => rs.Palestrante)
                .HasForeignKey(rs => rs.PalestranteId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
