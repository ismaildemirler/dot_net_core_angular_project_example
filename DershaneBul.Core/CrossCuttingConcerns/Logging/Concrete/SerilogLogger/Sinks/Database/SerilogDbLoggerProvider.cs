using FrameworkLogger = Microsoft.Extensions.Logging.ILogger;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Threading;
using Serilog;
using Microsoft.Extensions.Logging;

namespace DershaneBul.Core.CrossCuttingConcerns.Logging.Concrete.SerilogLogger.Sinks.Database
{
    [ProviderAlias("Serilog")]
    public class SerilogDbLoggerProvider : ILoggerProvider, ILogEventEnricher
    {
        internal const string OriginalFormatPropertyName = "{OriginalFormat}";
        internal const string ScopePropertyName = "Scope";
        
        readonly Serilog.ILogger _logger;
        readonly Action _dispose;

         public SerilogDbLoggerProvider(Serilog.ILogger logger = null,
            bool dispose = false)
        {
            if (logger != null)
                _logger = logger.ForContext(new[] { this });

            if (dispose)
            {
                if (logger != null)
                    _dispose = () => (logger as IDisposable)?.Dispose();
                else
                    _dispose = Log.CloseAndFlush;
            }
        }
        
        public FrameworkLogger CreateLogger(string name)
        {
            return new SerilogDbLogger(this, _logger, name);
        }
        
        public IDisposable BeginScope<T>(T state)
        {
            if (CurrentScope != null)
                return new SerilogLoggerScope(this, state);
            
            var popSerilogContext = LogContext.Push(this);
            return new SerilogLoggerScope(this, state, popSerilogContext);
        }
        
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            List<LogEventPropertyValue> scopeItems = null;
            for (var scope = CurrentScope; scope != null; scope = scope.Parent)
            {
                scope.EnrichAndCreateScopeItem(
                    logEvent, propertyFactory, out LogEventPropertyValue scopeItem);

                if (scopeItem != null)
                {
                    scopeItems = scopeItems ?? new List<LogEventPropertyValue>();
                    scopeItems.Add(scopeItem);
                }
            }

            if (scopeItems != null)
            {
                scopeItems.Reverse();
                logEvent.AddPropertyIfAbsent(new LogEventProperty(ScopePropertyName, new SequenceValue(scopeItems)));
            }
        }

        readonly AsyncLocal<SerilogLoggerScope> _value
            = new AsyncLocal<SerilogLoggerScope>();

        internal SerilogLoggerScope CurrentScope
        {
            get => _value.Value;
            set => _value.Value = value;
        }
        
        public void Dispose()
        {
            _dispose?.Invoke();
        }
    }
}
