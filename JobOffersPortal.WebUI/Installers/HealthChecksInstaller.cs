using JobOffersPortal.WebUI.Data;
using JobOffersPortal.WebUI.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.WebUI.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddDbContextCheck<DataContext>()
                    .AddCheck<RedisHealthCheck>("Redis");
        }
    }
}
