using GerenciadorCursos.Application.DTOs;
using GerenciadorCursos.Domain.Entities;
using GerenciadorCursos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Application.Mappings
{
    public class AlunoMapper : IDTOMapper<AlunoCreateDTO, Aluno>
    {
        public Aluno ToEntity(AlunoCreateDTO dto)
        {
           return new Aluno(dto.Cpf, dto.Nome, dto.Email, dto.DataNascimento);
        }

        public AlunoCreateDTO ToDTO(Aluno entity)
        {
            return new AlunoCreateDTO
            {
                Cpf = entity.Cpf,
                Nome = entity.Nome,
                Email = entity.Email,
                DataNascimento = entity.DataNascimento
            };
        }

        
    }
}
