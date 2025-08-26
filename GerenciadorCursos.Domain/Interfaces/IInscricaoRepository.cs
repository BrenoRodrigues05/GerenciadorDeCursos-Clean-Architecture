using GerenciadorCursos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Domain.Interfaces
{
    public interface IInscricaoRepository
    {
        Task<Inscricao> ObterPorIdAsync(int id);
        Task<IEnumerable<Inscricao>> ObterTodosAsync();

        Task AdicionarAsync(Inscricao inscricao);

        Task AtualizarAsync(Inscricao inscricao);

        Task RemoverAsync(int id);

        Task<Inscricao?> ObterPorAlunoCursoAsync(int alunoId, int cursoId);



    }
}
