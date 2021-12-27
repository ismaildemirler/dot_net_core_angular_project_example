using DershaneBul.Core.CrossCuttingConcerns.Logging.Concrete.SerilogLogger.Sinks.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DershaneBul.Core.Utilities.Extensions.LoggerExtensions.SerilogLogger
{
    public static class SerilogLoggingBuilderExtensions
    {
        public static ILoggingBuilder AddMyCustomSerilog(
            this ILoggingBuilder builder,
            Serilog.ILogger logger = null,
            bool dispose = false)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            if (dispose)
            {
                builder.Services.AddSingleton<
                    ILoggerProvider,
                    SerilogDbLoggerProvider>(services =>
                    new SerilogDbLoggerProvider(logger, true));
            }
            else
            {
                builder.AddProvider(new SerilogDbLoggerProvider(logger));
            }

            builder.AddFilter<SerilogDbLoggerProvider>(null, LogLevel.Trace);

            return builder;
        }
    }
}
