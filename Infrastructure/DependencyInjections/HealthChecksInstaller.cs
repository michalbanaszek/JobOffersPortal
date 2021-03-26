using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>();
        }
    }
}
