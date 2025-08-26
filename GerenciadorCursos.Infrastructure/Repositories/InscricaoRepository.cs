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
    public class InscricaoRepository : IInscricaoRepository
    {
        private readonly GerenciadorCursosContext _context;

        public InscricaoRepository(GerenciadorCursosContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Inscricao inscricao)
        {
            await _context.Inscricoes.AddAsync(inscricao);
        }

        public Task AtualizarAsync(Inscricao inscricao)
        {
          _context.Inscricoes.Update(inscricao);
            return Task.CompletedTask;
        }

        public async Task<Inscricao> ObterPorIdAsync(int id)
        {
           return await _context.Inscricoes.FindAsync(id);
        }

        public async Task<IEnumerable<Inscricao>> ObterTodosAsync()
        {
           return await _context.Inscricoes.ToListAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var inscricao = await _context.Inscricoes.FindAsync(id);
            if (inscricao != null)
                _context.Inscricoes.Remove(inscricao);
        }
        public async Task<Inscricao?> ObterPorAlunoCursoAsync(int alunoId, int cursoId)
        {
            return await _context.Inscricoes
                .FirstOrDefaultAsync(i => i.AlunoId == alunoId && i.CursoId == cursoId);
        }
    }
}
