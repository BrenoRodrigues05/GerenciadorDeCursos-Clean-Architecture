using GerenciadorCursos.Application.DTOs;
using GerenciadorCursos.Application.Handlers;
using GerenciadorCursos.Domain.Entities;
using GerenciadorCursos.Domain.Interfaces;
using GerenciadorCursos.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCursos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InscricaoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly CadastrarAlunoEmCursoHandler cadastrarAlunoEmCursoHandler;

       

        public InscricaoController(IUnitOfWork unitOfWork, CadastrarAlunoEmCursoHandler
            cadastrarAlunoEmCursoHandler)
        {
            _unitOfWork = unitOfWork;
            this.cadastrarAlunoEmCursoHandler = cadastrarAlunoEmCursoHandler;
           
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] InscricaoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var inscricao = await cadastrarAlunoEmCursoHandler.HandleAsync(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = inscricao.Id }, inscricao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var inscricoes = await _unitOfWork.Inscricoes.ObterTodosAsync();
            return Ok(inscricoes.Select(i => new InscricaoDTO
            {
                Id = i.Id,
                CursoId = i.CursoId,
                DataInscricao = i.DataInscricao
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var inscricao = await _unitOfWork.Inscricoes.ObterPorIdAsync(id);
            if (inscricao == null)
                return NotFound(new { message = "Inscrição não encontrada." });

            return Ok(new InscricaoDTO
            {
                Id = inscricao.Id,
                CursoId = inscricao.CursoId,
                DataInscricao = inscricao.DataInscricao
            });
        }

        [HttpGet("aluno/{alunoId}")]
        public async Task<IActionResult> ObterPorAluno(int alunoId)
        {
            var todas = await _unitOfWork.Inscricoes.ObterTodosAsync();
            var filtradas = todas.Where(i => i.AlunoId == alunoId)
                                 .Select(i => new InscricaoDTO
                                 {
                                     Id = i.Id,
                                     CursoId = i.CursoId,
                                     DataInscricao = i.DataInscricao
                                 });
            return Ok(filtradas);
        }
    }
}
