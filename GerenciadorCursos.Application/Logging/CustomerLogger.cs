using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace GerenciadorCursos.Application.Logging
{
    public class CustomerLogger : ILogger
    {
        private readonly string _name;
        private readonly CustomLoggerProviderConfiguration _config;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            _name = name;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state) => null!;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _config.Loglevel;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel) || formatter == null) return;

            try
            {
                string message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {_name} - {formatter(state, exception)}";
                if (exception != null)
                    message += $" | Exception: {exception.Message}";

                // Garante que a pasta exista
                string directory = Path.GetDirectoryName(_config.FilePath)!;
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // Escreve o log
                lock (this)
                {
                    File.AppendAllText(_config.FilePath, message + Environment.NewLine);
                }
            }
            catch
            {
                // Evita que falha no log quebre a aplicação
            }
        }
    }
}
