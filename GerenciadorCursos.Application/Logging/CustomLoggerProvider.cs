using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace GerenciadorCursos.Application.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomLoggerProviderConfiguration _config;
        private readonly ConcurrentDictionary<string, CustomerLogger> _loggers = new();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, _config));
        }

        public void Dispose() { }
    }
}
