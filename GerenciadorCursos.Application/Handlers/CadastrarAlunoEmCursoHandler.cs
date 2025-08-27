using GerenciadorCursos.Application.DTOs;
using GerenciadorCursos.Domain.Entities;
using GerenciadorCursos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Application.Handlers
{
    public class CadastrarAlunoEmCursoHandler
    {
        // Implementação do handler para cadastrar aluno em curso

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDTOMapper<InscricaoCreateDTO, Inscricao> _inscricaoMapper;
        public CadastrarAlunoEmCursoHandler(IUnitOfWork unitOfWork, IDTOMapper<InscricaoCreateDTO, 
            Inscricao> inscricaoMapper)
        {
            _unitOfWork = unitOfWork;
            _inscricaoMapper = inscricaoMapper;
        }
        public async Task<Inscricao> HandleAsync(InscricaoCreateDTO dto)
        {
            var aluno = await _unitOfWork.Alunos.ObterPorIdAsync(dto.AlunoId);
            if (aluno == null)
                throw new Exception("Aluno não encontrado.");

            var curso = await _unitOfWork.Cursos.ObterPorIdAsync(dto.CursoId);
            if (curso == null)
                throw new Exception("Curso não encontrado.");

            var jaInscrito = await _unitOfWork.Inscricoes.ObterPorAlunoCursoAsync(dto.AlunoId, dto.CursoId);
            if (jaInscrito != null)
                throw new Exception("Aluno já está inscrito neste curso.");

            var inscricao = _inscricaoMapper.ToEntity(dto);

            await _unitOfWork.Inscricoes.AdicionarAsync(inscricao);
            await _unitOfWork.CommitAsync();

            return inscricao;
        }

    }    
}
