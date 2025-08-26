using GerenciadorCursos.Application.DTOs;
using GerenciadorCursos.Application.Handlers;
using GerenciadorCursos.Domain.Entities;
using GerenciadorCursos.Domain.Interfaces;
using GerenciadorCursos.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCursos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
       private readonly CriarAlunoHandler _criarAlunoHandler;

        private readonly IUnitOfWork _unitOfWork;

        public AlunosController(IUnitOfWork unitOfWork, CriarAlunoHandler criarAlunoHandler)
        {
            _unitOfWork = unitOfWork;
            _criarAlunoHandler = criarAlunoHandler;
        }

        // Implementar endpoints para gerenciar alunos

        [HttpPost]

        public async Task<IActionResult> CriarAluno([FromBody] AlunoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Aluno aluno = await _criarAlunoHandler.HandleAsync(dto);
                return CreatedAtAction(nameof(ObterAlunoPorId), new { id = aluno.Id }, aluno);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObterTodosAlunos()
        {
            var alunos = await _unitOfWork.Alunos.ObterTodosAsync();
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterAlunoPorId(int id)
        {
            var aluno = await _unitOfWork.Alunos.ObterPorIdAsync(id);
            if (aluno == null)
                return NotFound(new { message = "Aluno não encontrado." });

            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAluno(int id, [FromBody] AlunoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var aluno = await _unitOfWork.Alunos.ObterPorIdAsync(id);
            if (aluno == null)
                return NotFound(new { message = "Aluno não encontrado." });

            aluno.AtualizarDados(dto.Cpf, dto.Nome, dto.Email, dto.DataNascimento);
            await _unitOfWork.Alunos.AtualizarAsync(aluno);
            await _unitOfWork.CommitAsync();

            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverAluno(int id)
        {
            var aluno = await _unitOfWork.Alunos.ObterPorIdAsync(id);
            if (aluno == null)
                return NotFound(new { message = "Aluno não encontrado." });

            await _unitOfWork.Alunos.RemoverAsync(id);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
