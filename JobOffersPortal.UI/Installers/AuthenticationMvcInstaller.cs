using JobOffersPortal.UI.ClientServices.Security;
using JobOffersPortal.UI.Interfaces;
using JobOffersPortal.UI.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.UI.Installers
{
    public class AuthenticationMvcInstaller : IMvcInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var ldapOptions = new LdapOptions();
            configuration.Bind(nameof(LdapOptions), ldapOptions);

            services.AddSingleton(ldapOptions);

            services.AddScoped<IAuthenticationLdapMvcService, AuthenticationLdapService>();
        }
    }
}
