using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace GerenciadorCursos.API.Filters
{
    public class APILoggingFilter : IActionFilter
    {
        private readonly ILogger<APILoggingFilter> _logger;
        private readonly Stopwatch _stopwatch = new();

        public APILoggingFilter(ILogger<APILoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Restart();
            var actionName = context.ActionDescriptor.DisplayName;
            var routeValues = string.Join(", ", context.ActionArguments.Select(a => $"{a.Key}={a.Value}"));

            _logger.LogInformation("Iniciando execução da action {Action}. Parâmetros: {Params}", actionName, routeValues);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var actionName = context.ActionDescriptor.DisplayName;
            var elapsedMs = _stopwatch.ElapsedMilliseconds;

            if (context.Exception == null)
            {
                _logger.LogInformation("Action {Action} executada com sucesso em {Elapsed}ms", actionName, elapsedMs);
            }
            else
            {
                _logger.LogError(context.Exception, "Action {Action} terminou com exceção após {Elapsed}ms", actionName, elapsedMs);
            }
        }
    }
}
