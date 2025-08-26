using GerenciadorCursos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Infrastructure.EntitiesConfigurations
{
    public class InscricaoConfiguration : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => new { i.AlunoId, i.CursoId }).IsUnique();

            builder.HasOne(i => i.Aluno)
                   .WithMany()
                   .HasForeignKey(i => i.AlunoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Curso)
                   .WithMany()
                   .HasForeignKey(i => i.CursoId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
