using GerenciadorCursos.Domain.Entities;
using GerenciadorCursos.Infrastructure.EntitiesConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Infrastructure.Context
{
    public class GerenciadorCursosContext : IdentityDbContext <IdentityUser>
    {
        public GerenciadorCursosContext(DbContextOptions<GerenciadorCursosContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoConfiguration());
            modelBuilder.ApplyConfiguration(new CursoConfiguration());
            modelBuilder.ApplyConfiguration(new InscricaoConfiguration());

            modelBuilder.Entity<Inscricao>()
           .HasIndex(i => new { i.AlunoId, i.CursoId })
           .IsUnique();

            base.OnModelCreating(modelBuilder);

        }

        // Definição dos DbSets para as entidades

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Inscricao> Inscricoes { get; set; }


    }
}
