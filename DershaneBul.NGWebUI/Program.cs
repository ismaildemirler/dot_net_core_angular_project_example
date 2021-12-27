using System;
using System.IO;
using DershaneBul.Core.Utilities.Extensions.LoggerExtensions.SerilogLogger;
using DershaneBul.Core.Utilities.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DershaneBul.NGWebUI
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            try
            {
                Log.Information("Uygulama başarıyla başlatıldı...");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Uygulama beklenmedik şekilde sonlandırıldı!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                .UseMyCustomSerilog((hostingContext, config) =>
                {
                    config.ReadFrom.Configuration(hostingContext.Configuration);
                },
                    preserveStaticLogger: true,
                    writeToProviders: true
                )
                .Build();
    }
}
