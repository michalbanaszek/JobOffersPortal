using Application.Common.Interfaces;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

            var redisCacheSettings = new RedisCacheOptions();
            configuration.GetSection(nameof(RedisCacheOptions)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
            {
                return;
            }

            services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
