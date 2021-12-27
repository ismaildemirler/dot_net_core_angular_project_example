using DershaneBul.Core.Utilities.Extensions;
using DershaneBul.Core.Utilities.Extensions.LoggerExtensions.SerilogLogger;
using DershaneBul.Core.Utilities.Helpers;
using DershaneBul.NGWebUI.Utilities.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DershaneBul.NGWebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CurrentDirectoryHelpers.SetCurrentDirectory();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration);
            services.AddLogging((builder) =>
            {
                builder.AddMyCustomSerilog(dispose: true);
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "DershaneBulClient/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.ConfigureCustomMiddlewares();
            //app.UseSerilogRequestLogging();
            app.UseMvcWithDefaultRoute();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "DershaneBulClient";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
