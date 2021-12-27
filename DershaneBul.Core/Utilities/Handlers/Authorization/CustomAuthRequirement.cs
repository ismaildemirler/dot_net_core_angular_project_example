using Microsoft.AspNetCore.Authorization;

namespace DershaneBul.Core.Utilities.Handlers.Authorization
{
    public class CustomAuthRequirement: IAuthorizationRequirement
    {
        public string Role { get; }
        public CustomAuthRequirement(string role)
        {
            Role = role;
        }
    }
}
