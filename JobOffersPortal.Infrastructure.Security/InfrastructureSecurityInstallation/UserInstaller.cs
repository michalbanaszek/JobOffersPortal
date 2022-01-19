using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Infrastructure.Security.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Infrastructure.Security.InfrastructureSecurityInstallation
{
    public class UserInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
