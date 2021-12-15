using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Persistance.EF.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Persistance.EF.InfrastructureInstallation
{
    public class RepositoriesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(BaseRepository<>));

            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IJobOfferRepository, JobOfferRepository>();

            services.AddScoped<IJobOfferSkillRepository, JobOfferSkillRepository>();

            services.AddScoped<IJobOfferPropositionRepository, JobOfferPropositionRepository>();

            services.AddScoped<IJobOfferRequirementRepository, JobOfferRequirementRepository>();
        }
    }
}