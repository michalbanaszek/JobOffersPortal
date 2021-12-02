using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Persistance.EF.Options;
using JobOffersPortal.Persistance.EF.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Persistance.EF.InfrastructureInstallation
{
    public class EmailInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
