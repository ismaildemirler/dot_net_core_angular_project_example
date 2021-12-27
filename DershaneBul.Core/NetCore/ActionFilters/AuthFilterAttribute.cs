using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DershaneBul.Core.NetCore.ActionFilters
{
    public class AuthFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // execute any code before the action executes
            var globalAttributes = context.ActionDescriptor.FilterDescriptors
                .Select(p => p.Filter);
            var anonymous = globalAttributes.OfType<AllowAnonymousFilter>().Count();
            if (anonymous == 0)
            {
                var controlerAttributes =
                    ((ControllerActionDescriptor)context.ActionDescriptor)
                    .MethodInfo?.DeclaringType?.GetCustomAttributes(true);
                var methodAttributes
                    = ((ControllerActionDescriptor)context.ActionDescriptor)
                    .MethodInfo?.GetCustomAttributes(true);

                var authAttributes = globalAttributes
                    .Union(controlerAttributes)
                    .Union(methodAttributes)
                    .OfType<AuthorizeAttribute>();

                if (authAttributes.Count() > 0)
                {
                    var expTime = context.HttpContext.User?
                    .FindFirst(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value;

                    long expiryDateUnix = 0;
                    if (expTime != null)
                    {
                        expiryDateUnix = long.Parse(expTime);
                    }

                    DateTime? expiryDateTime = null;
                    if (expiryDateUnix != 0)
                    {
                        expiryDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)
                            .AddSeconds(expiryDateUnix);
                    }

                    if (expiryDateTime == null)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }

                    if (expiryDateTime < DateTime.Now)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                }
            }

            await next();

            // execute any code after the action executes
        }
    }
}
