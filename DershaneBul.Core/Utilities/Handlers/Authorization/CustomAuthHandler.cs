using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DershaneBul.Core.Utilities.Handlers.Authorization
{
    public class CustomAuthHandler
        : AuthorizationHandler<CustomAuthRequirement>
    {
        private readonly IHttpContextAccessor _accessor;
        public CustomAuthHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomAuthRequirement requirement)
        {
            var userEmail = context.User?
                .FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
            if (!userEmail.EndsWith("hotmail.com"))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var role = context.User?
                    .FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
            if (!role.Contains("Admin"))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
