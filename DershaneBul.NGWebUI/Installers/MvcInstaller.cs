using DershaneBul.Core.NetCore.ActionFilters;
using DershaneBul.Core.Utilities.Handlers.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DershaneBul.NGWebUI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilterAttribute>();
                options.Filters.Add<AuthFilterAttribute>();
            });

            services.AddScoped<ValidationFilterAttribute>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("CustomAuthPolicy", policy =>
                {
                    policy.AddRequirements(
                        new CustomAuthRequirement("Admin"));
                });
            });
            services.AddScoped<IAuthorizationHandler, CustomAuthHandler>();
        }
    }
}
