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
    public class InscricaoMapper : IDTOMapper<InscricaoCreateDTO, Inscricao>
    {
        public Inscricao ToEntity(InscricaoCreateDTO dto)
        {
            return new Inscricao(dto.AlunoId, dto.CursoId);
        } 

        public InscricaoCreateDTO ToDTO(Inscricao entity)
        {
           return new InscricaoCreateDTO
            {
                AlunoId = entity.AlunoId,
                CursoId = entity.CursoId
            };
        }

       
    }
}
