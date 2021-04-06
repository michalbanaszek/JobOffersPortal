using Application.Common.Interfaces;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
{
    public class FacebookAuthInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var facebookAuthOptions = new FacebookAuthOptions();
            configuration.Bind(nameof(FacebookAuthOptions), facebookAuthOptions);
            services.AddSingleton(facebookAuthOptions);

            services.AddHttpClient();
        }
    }
}
