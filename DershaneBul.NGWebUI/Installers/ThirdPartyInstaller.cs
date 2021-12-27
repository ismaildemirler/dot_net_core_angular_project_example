using AutoMapper;
using DershaneBul.NGWebUI.Utilities.AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DershaneBul.NGWebUI.Installers
{
    public class ThirdPartyInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
