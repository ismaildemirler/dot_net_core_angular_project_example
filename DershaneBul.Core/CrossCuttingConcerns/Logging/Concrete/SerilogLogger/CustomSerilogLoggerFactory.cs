using DershaneBul.Core.CrossCuttingConcerns.Logging.Concrete.SerilogLogger.Sinks.Database;
using Microsoft.Extensions.Logging;
using Serilog.Debugging;
using System;
using System.ComponentModel;

namespace DershaneBul.Core.CrossCuttingConcerns.Logging.Concrete.SerilogLogger
{
    [Obsolete("Replaced with Serilog.Extensions.Logging.SerilogLoggerFactory")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class CustomSerilogLoggerFactory : ILoggerFactory
    {
        private readonly SerilogDbLoggerProvider _provider;

        public CustomSerilogLoggerFactory(
            Serilog.ILogger logger = null, bool dispose = false)
        {
            _provider = new SerilogDbLoggerProvider(logger, dispose);
        }
        
        public void Dispose()
        {
            _provider.Dispose();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _provider.CreateLogger(categoryName);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            SelfLog.WriteLine("Ignoring added logger provider {0}", provider);
        }
    }
}
