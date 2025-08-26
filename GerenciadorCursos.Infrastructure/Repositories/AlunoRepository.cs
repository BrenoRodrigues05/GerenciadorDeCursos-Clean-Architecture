using GerenciadorCursos.Domain.Entities;
using GerenciadorCursos.Domain.Interfaces;
using GerenciadorCursos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {

        private readonly GerenciadorCursosContext _context;

        public AlunoRepository(GerenciadorCursosContext context)
        {
            _context = context;
        }
        public async Task AdicionarAsync(Aluno aluno)
        {
           await _context.Alunos.AddAsync(aluno);
        }

        public async Task AtualizarAsync(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
        }

        public async Task<bool> ExisteAlunoComCpfAsync(int cpf)
        {
            return await _context.Alunos.AnyAsync(a => a.Cpf == cpf);
        }

        public async Task<Aluno> ObterPorIdAsync(int id)
        {
            return await _context.Alunos.FindAsync(id);
        }

        public async Task<IEnumerable<Aluno>> ObterTodosAsync()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
                _context.Alunos.Remove(aluno);
        }
    }
}
