using DershaneBul.Core.NetCore.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace DershaneBul.Core.Utilities.Extensions
{
    public static class MiddlewareExtension
    {
        public static void ConfigureCustomMiddlewares(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
