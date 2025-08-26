using GerenciadorCursos.Domain.Entities;
using GerenciadorCursos.Domain.Interfaces;
using GerenciadorCursos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly GerenciadorCursosContext _context;

        public CursoRepository(GerenciadorCursosContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Curso curso)
        {
            await _context.Cursos.AddAsync(curso);
        }

        public async Task AtualizarAsync(Curso curso)
        {
            _context.Cursos.Update(curso);
        }

        public async Task RemoverAsync(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
                _context.Cursos.Remove(curso);
        }

        public async Task<Curso> ObterPorIdAsync(int id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task<IEnumerable<Curso>> ObterTodosAsync()
        {
            return await _context.Cursos.ToListAsync();
        }

        public async Task<bool> ExisteCursoComNomeAsync(string nome)
        {
            return await _context.Cursos.AnyAsync(c => c.Nome == nome);
        }
    }
}
