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
    public class CriarAlunoHandler
    {
        // Implamentação de handler para criar perfil de aluno

        private readonly IUnitOfWork _unitOfWork;

        public CriarAlunoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Aluno> HandleAsync(AlunoCreateDTO dto)
        {
            if (await _unitOfWork.Alunos.ExisteAlunoComCpfAsync(dto.Cpf))

                throw new Exception("Já existe um aluno com este CPF.");
            var aluno = new Domain.Entities.Aluno(dto.Cpf, dto.Nome, dto.Email, dto.DataNascimento);

            await _unitOfWork.Alunos.AdicionarAsync(aluno);

            await _unitOfWork.CommitAsync(); // salva a transação

            return aluno;
        }
    }
}
