using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Application.DTOs
{
    public class InscricaoDTO
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public DateOnly DataInscricao { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
