using DershaneBul.Business.Abstract.Common;
using DershaneBul.Business.Abstract.Courses;
using DershaneBul.Business.Abstract.Identity;
using DershaneBul.Business.Abstract.Users;
using DershaneBul.Business.Concrete.Common;
using DershaneBul.Business.Concrete.Courses;
using DershaneBul.Business.Concrete.Firms;
using DershaneBul.Business.Concrete.Identity;
using DershaneBul.Business.Concrete.Users;
using DershaneBul.DataAccess.Abstract;
using DershaneBul.DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DershaneBul.NGWebUI.Installers
{
    public class BusinessInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IIdentityService, IdentityManager>();
            services.AddScoped<IFirmService, FirmManager>();
            services.AddScoped<IParameterService, ParameterManager>();
            services.AddScoped<ICommonService, CommonManager>();
        }
    }
}
