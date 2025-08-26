using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Application.DTOs
{
    public class CursoCreateDTO
    {
        public string Nome { get; set; }

        public int CargaHoraria { get; set; } // em horas

        public string Descricao { get; set; }
    }
}
