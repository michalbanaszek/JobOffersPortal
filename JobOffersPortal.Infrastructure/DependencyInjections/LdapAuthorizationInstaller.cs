using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
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
