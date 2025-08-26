using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Domain.Entities
{
    public class Inscricao
    {
        [Key]
        public int Id { get; private set; }   
        public int AlunoId { get; private set; }
        public Aluno Aluno { get; private set; }
        public int CursoId { get; private set; }
        public Curso Curso { get; private set; }
        public DateOnly DataInscricao { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        public Inscricao(int alunoId, int cursoId)
        {
            if (alunoId <= 0)
                throw new ArgumentException("O ID do aluno deve ser um número positivo.", nameof(alunoId));
            if (cursoId <= 0)
                throw new ArgumentException("O ID do curso deve ser um número positivo.", nameof(cursoId));
            AlunoId = alunoId;
            CursoId = cursoId;
            DataInscricao = DateOnly.FromDateTime(DateTime.Now);
        }

        // Construtor vazio exigido pelo EF Core
        private Inscricao() { }
    }
}
