using JobOffersPortal.API.Middleware;
using JobOffersPortal.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.API.Installers
{
    public class MiddlwareInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CustomExceptionHandlerMiddleware>();
        }
    }
}
