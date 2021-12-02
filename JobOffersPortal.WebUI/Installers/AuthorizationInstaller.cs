using JobOffersPortal.API.Security;
using JobOffersPortal.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.API.Installers
{
    public class AuthorizationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DomainNameRequirement", policy =>
                {
                    policy.AddRequirements(new WorksForCompanyRequirement("gmail.com"));
                });

                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            services.AddSingleton<IAuthorizationHandler, WorksForCompanyHandler>();
        }
    }
}
