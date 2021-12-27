using System;

namespace DershaneBul.Core.CrossCuttingConcerns.Logging.Concrete.SerilogLogger
{
    public class RequestLoggingOptions
    {
        public string MessageTemplate { get; }

        public RequestLoggingOptions(string messageTemplate)
        {
            MessageTemplate = messageTemplate
                ?? throw new ArgumentNullException(nameof(messageTemplate));
        }
    }
}
