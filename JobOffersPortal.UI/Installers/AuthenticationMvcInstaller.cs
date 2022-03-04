﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.ClientServices.Security;
using WebApp.Interfaces;
using WebApp.Options;

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
