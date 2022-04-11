using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Persistance.EF.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.API.Installers
{
    public class SeederInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ApplicationDbContextSeed>();
        }
    }
}
