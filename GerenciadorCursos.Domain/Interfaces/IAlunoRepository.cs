using GerenciadorCursos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Domain.Interfaces
{
    public interface IAlunoRepository
    {
        Task<Aluno> ObterPorIdAsync(int id);

        Task<IEnumerable<Aluno>> ObterTodosAsync();

        Task AdicionarAsync(Aluno aluno);

        Task AtualizarAsync(Aluno aluno);

        Task RemoverAsync(int id);

        Task<bool> ExisteAlunoComCpfAsync(int cpf);


    }
}
