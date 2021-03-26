using Application.Common.Interfaces;
using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
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
        }
    }
}
