using GerenciadorCursos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Domain.Interfaces
{
    public interface ICursoRepository
    {
        Task<Curso> ObterPorIdAsync(int id);
        Task<IEnumerable<Curso>> ObterTodosAsync();
        Task AdicionarAsync(Curso curso);
        Task AtualizarAsync(Curso curso);
        Task RemoverAsync(int id);
        Task<bool> ExisteCursoComNomeAsync(string nome);
    }
}
