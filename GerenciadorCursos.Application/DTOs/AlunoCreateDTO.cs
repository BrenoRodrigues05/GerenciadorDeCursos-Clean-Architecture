using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Application.DTOs
{
    public class AlunoCreateDTO
    {
        public int Cpf { get; set; }
        public string Nome { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateOnly DataNascimento { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
