using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrameworkLogger = Microsoft.Extensions.Logging.ILogger;

namespace DershaneBul.Core.CrossCuttingConcerns.Logging.Concrete.SerilogLogger.Sinks.Database
{
    public class SerilogDbLogger : FrameworkLogger
    {
        readonly SerilogDbLoggerProvider _provider;
        readonly ILogger _logger;

        static readonly MessageTemplateParser MessageTemplateParser = new MessageTemplateParser();

        // It's rare to see large event ids, as they are category-specific
        static readonly LogEventProperty[] LowEventIdValues = Enumerable.Range(0, 48)
            .Select(n => new LogEventProperty("Id", new ScalarValue(n)))
            .ToArray();

        public SerilogDbLogger(
            SerilogDbLoggerProvider provider,
            ILogger logger = null,
            string name = null)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _logger = logger;

            // If a logger was passed, the provider has already added itself as an enricher
            _logger = _logger ?? _logger.ForContext(new[] { provider });

            if (name != null)
            {
                _logger = _logger.ForContext(Constants.SourceContextPropertyName, name);
            }
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return _logger.IsEnabled(LevelConvert.ToSerilogLevel(logLevel));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _provider.BeginScope(state);
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel,
            Microsoft.Extensions.Logging.EventId eventId,
            TState state, Exception exception, Func<TState,
            Exception, string> formatter)
        {
            var level = LevelConvert.ToSerilogLevel(logLevel);
            if (!_logger.IsEnabled(level))
            {
                return;
            }

            var logger = _logger;
            string messageTemplate = null;

            var properties = new List<LogEventProperty>();

            if (state is IEnumerable<KeyValuePair<string, object>> structure)
            {
                foreach (var property in structure)
                {
                    if (property.Key == SerilogDbLoggerProvider.OriginalFormatPropertyName && property.Value is string value)
                    {
                        messageTemplate = value;
                    }
                    else if (property.Key.StartsWith("@"))
                    {
                        if (logger.BindProperty(property.Key.Substring(1), property.Value, true, out var destructured))
                            properties.Add(destructured);
                    }
                    else
                    {
                        if (logger.BindProperty(property.Key, property.Value, false, out var bound))
                            properties.Add(bound);
                    }
                }

                var stateType = state.GetType();
                var stateTypeInfo = stateType.GetTypeInfo();
                // Imperfect, but at least eliminates `1 names
                if (messageTemplate == null && !stateTypeInfo.IsGenericType)
                {
                    messageTemplate = "{" + stateType.Name + ":l}";
                    if (logger.BindProperty(stateType.Name, AsLoggableValue(state, formatter), false, out var stateTypeProperty))
                        properties.Add(stateTypeProperty);
                }
            }

            if (messageTemplate == null)
            {
                string propertyName = null;
                if (state != null)
                {
                    propertyName = "State";
                    messageTemplate = "{State:l}";
                }
                else if (formatter != null)
                {
                    propertyName = "Message";
                    messageTemplate = "{Message:l}";
                }

                if (propertyName != null)
                {
                    if (logger.BindProperty(propertyName, AsLoggableValue(state, formatter), false, out var property))
                        properties.Add(property);
                }
            }

            if (eventId.Id != 0 || eventId.Name != null)
                properties.Add(CreateEventIdProperty(eventId));

            var parsedTemplate = MessageTemplateParser.Parse(messageTemplate ?? "");
            var evt = new LogEvent(DateTime.Now, level, exception, parsedTemplate, properties);
            logger.Write(evt);
        }

        static object AsLoggableValue<TState>(TState state, Func<TState, Exception, string> formatter)
        {
            object sobj = state;
            if (formatter != null)
                sobj = formatter(state, null);
            return sobj;
        }

        internal static LogEventProperty CreateEventIdProperty(
            Microsoft.Extensions.Logging.EventId eventId)
        {
            var properties = new List<LogEventProperty>(2);

            if (eventId.Id != 0)
            {
                if (eventId.Id >= 0 && eventId.Id < LowEventIdValues.Length)
                    // Avoid some allocations
                    properties.Add(LowEventIdValues[eventId.Id]);
                else
                    properties.Add(new LogEventProperty("Id", new ScalarValue(eventId.Id)));
            }

            if (eventId.Name != null)
            {
                properties.Add(new LogEventProperty("Name", new ScalarValue(eventId.Name)));
            }

            return new LogEventProperty("EventId", new StructureValue(properties));
        }
    }
}
