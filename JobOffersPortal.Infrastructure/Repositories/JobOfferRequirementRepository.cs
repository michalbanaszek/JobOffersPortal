using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using JobOffersPortal.Persistance.EF.Persistence;

namespace JobOffersPortal.Persistance.EF.Repositories
{
    public class JobOfferRequirementRepository : BaseRepository<JobOfferRequirement>, IJobOfferRequirementRepository
    {
        public JobOfferRequirementRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
