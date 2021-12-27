using DershaneBul.DataAccess.Abstract.Common;
using DershaneBul.DataAccess.Abstract.Firms;
using DershaneBul.DataAccess.Abstract.Identity;
using DershaneBul.DataAccess.Abstract.Parameter;
using DershaneBul.DataAccess.Concrete.EntityFramework.Common;
using DershaneBul.DataAccess.Concrete.EntityFramework.Firms;
using DershaneBul.DataAccess.Concrete.EntityFramework.Identity;
using DershaneBul.DataAccess.Concrete.EntityFramework.Parameter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DershaneBul.NGWebUI.Installers
{
    public class DalInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFirmDAL, EfFirmDAL>();
            services.AddScoped<IIdentityDAL, EfIdentityDAL>();
            services.AddScoped<IParameterDAL, EfParameterDAL>();
            services.AddScoped<ICommonDAL, EfCommonDAL>();
        }
    }
}
