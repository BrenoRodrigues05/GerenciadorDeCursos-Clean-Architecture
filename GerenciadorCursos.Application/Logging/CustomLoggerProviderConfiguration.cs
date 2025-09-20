using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Application.Logging
{
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel Loglevel { get; set; } = LogLevel.Warning;
        public string FilePath { get; set; } = @"C:\Users\CSM\Desktop\CURSOS\ASP NET CORE\LOGGING\Cursos.txt";
    }
}
