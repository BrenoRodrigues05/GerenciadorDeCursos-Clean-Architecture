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
    public class CriarCursoHandler
    {
        // Implementação do handler para criar um curso

       private readonly IUnitOfWork _unitOfWork;
        private readonly IDTOMapper<CursoCreateDTO, Curso> _cursoMapper;
        public CriarCursoHandler(IUnitOfWork unitOfWork, IDTOMapper<CursoCreateDTO, Curso> cursoMapper)
        {
            _unitOfWork = unitOfWork;
            _cursoMapper = cursoMapper;
        }

        public async Task<Curso> HandleAsync(CursoCreateDTO dto)
        {
            if (await _unitOfWork.Cursos.ExisteCursoComNomeAsync(dto.Nome))
                throw new System.Exception("Já existe um curso com este nome.");

            var curso = _cursoMapper.ToEntity(dto);

            await _unitOfWork.Cursos.AdicionarAsync(curso);
            await _unitOfWork.CommitAsync(); // salva a transação

            return curso;
        }
    }
}
