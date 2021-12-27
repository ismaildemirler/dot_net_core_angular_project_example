
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DershaneBul.NGWebUI.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
