using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Persistence.Map
{
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Local).IsRequired().HasMaxLength(250);
            builder.Property(e => e.DataEvento).IsRequired();
            builder.Property(e => e.Tema).IsRequired().HasMaxLength(250);
            builder.Property(e => e.QtdPessoas).IsRequired();
            builder.Property(e => e.ImagemURL).IsRequired().HasMaxLength(250);
            builder.HasMany(e => e.Lotes)
                .WithOne(l => l.Evento)
                .HasForeignKey(l => l.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(e => e.RedeSociais)
                .WithOne(rs => rs.Evento)
                .HasForeignKey(rs => rs.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(e => e.Palestrantes)
                .WithMany(pe => pe.Eventos)
                .UsingEntity<Dictionary<string, object>>(
                  "PalestranteEventos",
                  j => j.HasOne<Palestrante>().WithMany().HasForeignKey("PalestranteId").OnDelete(DeleteBehavior.Cascade),
                  j => j.HasOne<Evento>().WithMany().HasForeignKey("EventoId").OnDelete(DeleteBehavior.Cascade),

                  j =>
                    {
                        j.HasKey("EventoId", "PalestranteId");
                        j.ToTable("PalestranteEventos");
                    }
                );
            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
