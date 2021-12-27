using DershaneBul.Core.CrossCuttingConcerns.Logging.Concrete.SerilogLogger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System;

namespace DershaneBul.Core.Utilities.Extensions.LoggerExtensions.SerilogLogger
{
    public static class SerilogWebHostBuilderExtensions
    {
        public static IWebHostBuilder UseMyCustomSerilog(
            this IWebHostBuilder builder,
            Serilog.ILogger logger = null,
            bool dispose = false,
            LoggerProviderCollection providers = null)
        {
            if (builder == null) throw new
                    ArgumentNullException(nameof(builder));

            builder.ConfigureServices(collection =>
            {
                if (providers != null)
                {
                    collection.AddSingleton<ILoggerFactory>(services =>
                    {
                        var factory = new CustomSerilogLoggerFactory(logger, dispose);

                        foreach (var provider in services.GetServices<ILoggerProvider>())
                            factory.AddProvider(provider);

                        return factory;
                    });
                }
                else
                {
                    collection.AddSingleton<ILoggerFactory>(
                        services => new CustomSerilogLoggerFactory(logger, dispose));
                }

                ConfigureServices(collection, logger);
            });

            return builder;
        }

        public static IWebHostBuilder UseMyCustomSerilog(
             this IWebHostBuilder builder,
             Action<WebHostBuilderContext, LoggerConfiguration> configureLogger,
             bool preserveStaticLogger = false,
             bool writeToProviders = false)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (configureLogger == null) throw new ArgumentNullException(nameof(configureLogger));

            builder.ConfigureServices((context, collection) =>
            {
                var loggerConfiguration = new LoggerConfiguration();

                LoggerProviderCollection loggerProviders = null;
                if (writeToProviders)
                {
                    loggerProviders = new LoggerProviderCollection();
                    loggerConfiguration.WriteTo.Providers(loggerProviders);
                }

                configureLogger(context, loggerConfiguration);
                var logger = loggerConfiguration.CreateLogger();

                Serilog.ILogger registeredLogger = null;
                if (preserveStaticLogger)
                {
                    registeredLogger = logger;
                }
                else
                {
                    Log.Logger = logger;
                }

                collection.AddSingleton<ILoggerFactory>(services =>
                {
                    var factory = new CustomSerilogLoggerFactory(registeredLogger, true);

                    if (writeToProviders)
                    {
                        foreach (var provider in services.GetServices<ILoggerProvider>())
                            factory.AddProvider(provider);
                    }

                    return factory;
                });

                ConfigureServices(collection, logger);
            });
            return builder;
        }

        static void ConfigureServices(IServiceCollection collection,
            Serilog.ILogger logger)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            if (logger != null)
            {
                collection.AddSingleton(logger);
            }
            
            var diagnosticContext = new Serilog.Extensions.Hosting.DiagnosticContext(logger);
            
            collection.AddSingleton(diagnosticContext);
            
            collection.AddSingleton<IDiagnosticContext>(diagnosticContext);
        }
    }
}
