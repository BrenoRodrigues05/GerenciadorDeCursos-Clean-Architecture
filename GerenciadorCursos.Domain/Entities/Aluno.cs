using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Domain.Entities
{
    [Table("Alunos")]
    public class Aluno
    {
        public int Id { get; private set; }

        public int Cpf { get; private set; }

        public string Nome { get; private set; }
        [EmailAddress]
        public string Email { get; private set; }

        public DateOnly DataNascimento { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        public Aluno(int cpf, string nome, string email, DateOnly dataNascimento)
        {
            if (cpf <= 0)
                throw new ArgumentException("O CPF deve ser um número positivo.", nameof(cpf));
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do aluno não pode ser vazio.", nameof(nome));
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("O email do aluno é inválido.", nameof(email));
            if (dataNascimento >= DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("A data de nascimento deve ser uma data passada.", nameof
                    (dataNascimento));
            Cpf = cpf;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
        }

        public void AtualizarDados(int cpf, string nome, string email, DateOnly dataNascimento)
        {
            if (cpf <= 0)
                throw new ArgumentException("O CPF deve ser um número positivo.", nameof(cpf));
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do aluno não pode ser vazio.", nameof(nome));
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("O email do aluno é inválido.", nameof(email));
            if (dataNascimento >= DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("A data de nascimento deve ser uma data passada.", nameof
                    (dataNascimento));
            Cpf = cpf;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
        }
    }
}
