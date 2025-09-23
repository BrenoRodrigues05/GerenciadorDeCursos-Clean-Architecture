using GerenciadorCursos.Application.DTOs;
using GerenciadorCursos.Application.Handlers;
using GerenciadorCursos.Domain.Entities;
using GerenciadorCursos.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GerenciadorCursos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlunosController : ControllerBase
    {
        private readonly CriarAlunoHandler _criarAlunoHandler;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AlunosController> _logger;

        public AlunosController(IUnitOfWork unitOfWork, CriarAlunoHandler criarAlunoHandler, ILogger<AlunosController> logger)
        {
            _unitOfWork = unitOfWork;
            _criarAlunoHandler = criarAlunoHandler;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CriarAluno([FromBody] AlunoCreateDTO dto)
        {
            _logger.LogInformation("Iniciando criação de aluno. DTO: {@DTO}", dto);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Falha na validação do DTO: {@DTO}", dto);
                return BadRequest(ModelState);
            }

            try
            {
                Aluno aluno = await _criarAlunoHandler.HandleAsync(dto);
                _logger.LogInformation("Aluno criado com sucesso. ID: {Id}", aluno.Id);
                return CreatedAtAction(nameof(ObterAlunoPorId), new { id = aluno.Id }, aluno);
            }
            catch (Exception ex)
            {
                // Usando string literal + parâmetro, nada de dto.ToString
                _logger.LogError(ex, "Erro ao criar aluno. DTO enviado: {@DTO}", dto);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosAlunos()
        {
            _logger.LogInformation("Consultando todos os alunos.");

            var alunos = (await _unitOfWork.Alunos.ObterTodosAsync()).ToList();

            _logger.LogInformation($"Total de alunos encontrados: {alunos.Count}");

            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterAlunoPorId(int id)
        {
            _logger.LogInformation("Consultando aluno por ID: {Id}", id);
            var aluno = await _unitOfWork.Alunos.ObterPorIdAsync(id);

            if (aluno == null)
            {
                _logger.LogWarning("Aluno não encontrado. ID: {Id}", id);
                return NotFound(new { message = "Aluno não encontrado." });
            }

            _logger.LogInformation("Aluno encontrado. ID: {Id}", id);
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAluno(int id, [FromBody] AlunoCreateDTO dto)
        {
            _logger.LogInformation("Iniciando atualização do aluno. ID: {Id}, DTO: {@DTO}", id, dto);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Falha na validação ao atualizar aluno. ID: {Id}, DTO: {@DTO}", id, dto);
                return BadRequest(ModelState);
            }

            var aluno = await _unitOfWork.Alunos.ObterPorIdAsync(id);
            if (aluno == null)
            {
                _logger.LogWarning("Aluno não encontrado para atualização. ID: {Id}", id);
                return NotFound(new { message = "Aluno não encontrado." });
            }

            try
            {
                aluno.AtualizarDados(dto.Cpf, dto.Nome, dto.Email, dto.DataNascimento);
                await _unitOfWork.Alunos.AtualizarAsync(aluno);
                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Aluno atualizado com sucesso. ID: {Id}", id);
                return Ok(aluno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar aluno. ID: {Id}, DTO enviado: {@DTO}", id, dto);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverAluno(int id)
        {
            _logger.LogInformation("Tentando remover aluno. ID: {Id}", id);
            var aluno = await _unitOfWork.Alunos.ObterPorIdAsync(id);

            if (aluno == null)
            {
                _logger.LogWarning("Aluno não encontrado para remoção. ID: {Id}", id);
                return NotFound(new { message = "Aluno não encontrado." });
            }

            try
            {
                await _unitOfWork.Alunos.RemoverAsync(id);
                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Aluno removido com sucesso. ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover aluno. ID: {Id}", id);
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
