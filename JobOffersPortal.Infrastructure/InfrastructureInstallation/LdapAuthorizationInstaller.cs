using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Persistance.EF.Identity;
using JobOffersPortal.Persistance.EF.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Persistance.EF.InfrastructureInstallation
{
    public class LdapAuthorizationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LdapOptions>(configuration.GetSection("ldap"));
            services.AddScoped<IAuthenticationService, LdapAuthenticationService>();

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
