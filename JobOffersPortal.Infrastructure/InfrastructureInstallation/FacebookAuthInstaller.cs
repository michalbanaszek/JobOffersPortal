using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Persistance.EF.Options;
using JobOffersPortal.Persistance.EF.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Persistance.EF.InfrastructureInstallation
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
