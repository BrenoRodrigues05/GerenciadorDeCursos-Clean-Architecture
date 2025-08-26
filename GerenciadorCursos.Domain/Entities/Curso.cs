using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Domain.Entities
{
    [Table("Cursos")]
    public  class Curso
    {
        public int Id { get; private set; }

        public string Nome { get; private set; }

        public int CargaHoraria { get; private set; } // em horas

        public string Descricao { get; private set; }

        // Construtor para criar o curso com todos os atributos
        public Curso(string nome, int cargaHoraria, string descricao = "")
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do curso não pode ser vazio.", nameof(nome));

            if (cargaHoraria <= 20)
                throw new ArgumentException("A carga horária deve ser maior que 20 horas.", nameof(cargaHoraria));

            Nome = nome;
            CargaHoraria = cargaHoraria;
            Descricao = descricao;
        }

        // Método para atualizar os dados do curso
        public void Atualizar(string nome, int cargaHoraria, string descricao = "")
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do curso não pode ser vazio.", nameof(nome));
            if (cargaHoraria <= 20)
                throw new ArgumentException("A carga horária deve ser maior que 20 horas.", nameof(cargaHoraria));
            Nome = nome;
            CargaHoraria = cargaHoraria;
            Descricao = descricao;
        }
    }
}
