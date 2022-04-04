using JobOffersPortal.UI.ClientServices;
using JobOffersPortal.UI.ClientServices.Security;
using JobOffersPortal.UI.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.UI.Installers
{
    public class ServicesMvcInstaller : IMvcInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityMvcService, IdentityMvcService>();
            services.AddScoped<IUserMvcService, UserMvcService>();
            services.AddScoped<IAddBearerTokenMvcService, AddBearerTokenService>();
            services.AddScoped<IJobOfferMvcService, JobOfferMvcService>();
            services.AddScoped<IJobOfferPropositionMvcService, JobOfferPropositionMvcService>();
            services.AddScoped<IJobOfferSkillMvcService, JobOfferSkillMvcService>();
            services.AddScoped<IJobOfferRequirementMvcService, JobOfferRequirementMvcService>();
        }
    }
}
