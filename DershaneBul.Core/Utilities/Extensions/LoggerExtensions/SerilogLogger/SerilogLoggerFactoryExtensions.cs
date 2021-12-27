using DershaneBul.Core.CrossCuttingConcerns.Logging.Concrete.SerilogLogger.Sinks.Database;
using Microsoft.Extensions.Logging;
using System;

namespace DershaneBul.Core.Utilities.Extensions.LoggerExtensions.SerilogLogger
{
    public static class SerilogLoggerFactoryExtensions
    {
        public static ILoggerFactory AddMyCustomSerilog(
            this ILoggerFactory factory,
            Serilog.ILogger logger = null,
            bool dispose = false)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            factory.AddProvider(new SerilogDbLoggerProvider(logger, dispose));

            return factory;
        }
    }
}
