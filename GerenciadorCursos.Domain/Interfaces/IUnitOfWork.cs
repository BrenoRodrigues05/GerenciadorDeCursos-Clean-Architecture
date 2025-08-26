using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICursoRepository Cursos { get; }   // expõe o repositório

        IAlunoRepository Alunos { get;  }     // expõe o repositório

        IInscricaoRepository Inscricoes { get; } // expõe o repositório
        Task<int> CommitAsync();              // salva todas as alterações

    }
}
