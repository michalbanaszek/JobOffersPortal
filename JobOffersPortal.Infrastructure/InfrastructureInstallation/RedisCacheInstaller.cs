using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Persistance.EF.Options;
using JobOffersPortal.Persistance.EF.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Persistance.EF.InfrastructureInstallation
{
    public class RedisCacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheOptions();

            configuration.GetSection(nameof(RedisCacheOptions)).Bind(redisCacheSettings);

            services.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
            {
                return;
            }

            services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);

            services.AddSingleton<ICacheService, RedisCacheService>();
        }
    }
}
