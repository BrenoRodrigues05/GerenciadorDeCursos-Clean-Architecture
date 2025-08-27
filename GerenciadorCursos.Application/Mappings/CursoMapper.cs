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
    public class CursoMapper : IDTOMapper<CursoCreateDTO, Curso>
    {
        public Curso ToEntity(CursoCreateDTO dto)
        {
           return new Curso(dto.Nome, dto.CargaHoraria, dto.Descricao);
        }

        public CursoCreateDTO ToDTO(Curso entity)
        {
            return new CursoCreateDTO
            {
                Nome = entity.Nome,
                CargaHoraria = entity.CargaHoraria,
                Descricao = entity.Descricao
            };
        }

        
    }
}
