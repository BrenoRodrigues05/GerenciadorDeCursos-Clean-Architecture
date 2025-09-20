using GerenciadorCursos.Application.DTOs;
using GerenciadorCursos.Application.Handlers;
using GerenciadorCursos.Domain.Entities;
using GerenciadorCursos.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCursos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CursosController : ControllerBase
    {
        private readonly CriarCursoHandler _criarCursoHandler;
        private readonly IUnitOfWork _unitOfWork;
       
        public CursosController(CriarCursoHandler criarCursoHandler, IUnitOfWork unitOfWork)
            
        {
            _criarCursoHandler = criarCursoHandler;
            _unitOfWork = unitOfWork;
           
        }

        // Endpoint para criar um curso

        [HttpPost]

        public async Task<IActionResult> CriarCurso([FromBody] CursoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Curso curso = await _criarCursoHandler.HandleAsync(dto);
                return CreatedAtAction(nameof(ObterCursoPorId), new { id = curso.Id }, curso);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> ObterTodos()
        {
            var cursos = await _unitOfWork.Cursos.ObterTodosAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> ObterCursoPorId(int id)
        {
            var curso = await _unitOfWork.Cursos.ObterPorIdAsync(id);
            if (curso == null)
                return NotFound();

            return Ok(curso);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCurso(int id, [FromBody] CursoCreateDTO dto)
        {
            var curso = await _unitOfWork.Cursos.ObterPorIdAsync(id);
            if (curso == null)
                return NotFound();

            curso.Atualizar(dto.Nome, dto.CargaHoraria, dto.Descricao);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverCurso(int id)
        {
            var curso = await _unitOfWork.Cursos.ObterPorIdAsync(id);
            if (curso == null)
                return NotFound();

            await _unitOfWork.Cursos.RemoverAsync(id);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
