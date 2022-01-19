using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Infrastructure.Security.Authorization.DomainNameForEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Infrastructure.Security.InfrastructureSecurityInstallation
{
    public class AuthorizationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DomainNameRequirement", policy =>
                {
                    policy.AddRequirements(new DomainNameForEmailRequirement("gmail.com"));
                });

                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            services.AddSingleton<IAuthorizationHandler, DomainNameForEmailHandler>();
        }
    }
}
