using JobOffersPortal.WebUI.Options;
using JobOffersPortal.WebUI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.WebUI.Installers
{
    public class FacebookAuthInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var facebookAuthOptions = new FacebookAuthOptions();
            configuration.Bind(nameof(FacebookAuthOptions), facebookAuthOptions);
            services.AddSingleton(facebookAuthOptions);

            services.AddHttpClient();
            services.AddSingleton<IFacebookAuthService, FacebookAuthService>();
        }
    }
}
