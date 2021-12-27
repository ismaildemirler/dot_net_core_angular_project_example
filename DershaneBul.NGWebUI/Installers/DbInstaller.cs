using DershaneBul.DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DershaneBul.NGWebUI.Installers
{
    public class DbInstaller: IInstaller
    {
        public void InstallServices(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<DershaneBulDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
