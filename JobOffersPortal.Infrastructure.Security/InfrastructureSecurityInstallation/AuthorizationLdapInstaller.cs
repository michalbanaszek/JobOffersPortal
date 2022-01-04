using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Infrastructure.Security.Options;
using JobOffersPortal.Infrastructure.Security.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Infrastructure.Security.InfrastructureSecurityInstallation
{
    public class AuthorizationLdapInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var ldapOptions = new LdapOptions();

            configuration.Bind(nameof(LdapOptions), ldapOptions);

            services.AddSingleton(ldapOptions);

            services.AddScoped<IAuthenticationLdapService, AuthenticationLdapService>();

            //policies are added in the account controller, after a valid login
            services.AddAuthorization(options =>
            {
                options.AddPolicy("LdapPolicy", policy =>
                                  policy.RequireClaim("Read", "true")
                                        .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                                      );
            });
        }
    }
}
