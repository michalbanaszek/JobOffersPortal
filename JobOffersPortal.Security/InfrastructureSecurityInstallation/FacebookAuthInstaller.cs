using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Infrastructure.Security.Options;
using JobOffersPortal.Infrastructure.Security.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Infrastructure.Security.InfrastructureSecurityInstallation
{
    public class FacebookAuthInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var facebookAuthOptions = new FacebookAuthOptions();

            configuration.Bind(nameof(FacebookAuthOptions), facebookAuthOptions);

            services.AddSingleton(facebookAuthOptions);

            services.AddSingleton<IFacebookAuthService, FacebookAuthService>();

            services.AddHttpClient();
        }
    }
}
