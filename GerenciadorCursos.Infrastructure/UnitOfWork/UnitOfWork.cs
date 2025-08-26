using GerenciadorCursos.Domain.Interfaces;
using GerenciadorCursos.Infrastructure.Context;
using GerenciadorCursos.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Infrastructure.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GerenciadorCursosContext _context;
        private CursoRepository _cursoRepository;
        private AlunoRepository _alunoRepository;
        private InscricaoRepository _inscricaoRepository;

        public UnitOfWork(GerenciadorCursosContext context)
        {
            _context = context;
        }

        public ICursoRepository Cursos => _cursoRepository ??= new CursoRepository(_context);

        public IAlunoRepository Alunos => _alunoRepository ??= new AlunoRepository(_context);

        public IInscricaoRepository Inscricoes => _inscricaoRepository ??= new InscricaoRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
