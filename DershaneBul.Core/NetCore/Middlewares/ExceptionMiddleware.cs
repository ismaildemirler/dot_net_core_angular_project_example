using DershaneBul.Core.Entities.Enums;
using DershaneBul.Entities.Containers.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DershaneBul.Core.NetCore.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger
            )
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"Something went wrong: {ex}", ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = (exception as WebException != null &&
                        ((HttpWebResponse)(exception as WebException).Response) != null) ?
                         ((HttpWebResponse)(exception as WebException).Response).StatusCode
                         : getErrorCode(exception.GetType());

            string message = exception.Message;

            await context.Response.WriteAsync(new BaseResponse
            {
                StatusCode = statusCode,
                Message = message,
                Details = new[] { exception.StackTrace }
            }.ToString());
        }

        private HttpStatusCode getErrorCode(Type exceptionType)
        {
            EnumExceptions tryParseResult;
            if (Enum.TryParse(exceptionType.Name, out tryParseResult))
            {
                switch (tryParseResult)
                {
                    case EnumExceptions.NullReferenceException:
                        return HttpStatusCode.LengthRequired;

                    case EnumExceptions.FileNotFoundException:
                        return HttpStatusCode.NotFound;

                    case EnumExceptions.OverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case EnumExceptions.OutOfMemoryException:
                        return HttpStatusCode.ExpectationFailed;

                    case EnumExceptions.InvalidCastException:
                        return HttpStatusCode.PreconditionFailed;

                    case EnumExceptions.ObjectDisposedException:
                        return HttpStatusCode.Gone;

                    case EnumExceptions.UnauthorizedAccessException:
                        return HttpStatusCode.Unauthorized;

                    case EnumExceptions.NotImplementedException:
                        return HttpStatusCode.NotImplemented;

                    case EnumExceptions.NotSupportedException:
                        return HttpStatusCode.NotAcceptable;

                    case EnumExceptions.InvalidOperationException:
                        return HttpStatusCode.MethodNotAllowed;

                    case EnumExceptions.TimeoutException:
                        return HttpStatusCode.RequestTimeout;

                    case EnumExceptions.ArgumentException:
                        return HttpStatusCode.BadRequest;

                    case EnumExceptions.StackOverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case EnumExceptions.FormatException:
                        return HttpStatusCode.UnsupportedMediaType;

                    case EnumExceptions.IOException:
                        return HttpStatusCode.NotFound;

                    case EnumExceptions.IndexOutOfRangeException:
                        return HttpStatusCode.ExpectationFailed;

                    default:
                        return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
