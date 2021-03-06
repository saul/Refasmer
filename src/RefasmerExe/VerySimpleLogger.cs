using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace JetBrains.Refasmer
{
    public class VerySimpleLogger: ILogger
    {
        private readonly TextWriter _writer;
        private readonly LogLevel _level;
        

        public VerySimpleLogger(Stream stream, LogLevel level = LogLevel.Trace)
        {
            _level = level;
            _writer = new StreamWriter(stream);
        }

        public VerySimpleLogger(TextWriter writer, LogLevel level = LogLevel.Trace)
        {
            _writer = writer;
            _level = level;

        }
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
                _writer.WriteLine(formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _level;

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}