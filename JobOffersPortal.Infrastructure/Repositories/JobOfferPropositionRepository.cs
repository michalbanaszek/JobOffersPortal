using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using JobOffersPortal.Persistance.EF.Persistence;

namespace JobOffersPortal.Persistance.EF.Repositories
{
    public class JobOfferPropositionRepository : BaseRepository<JobOfferProposition>, IJobOfferPropositionRepository
    {
        public JobOfferPropositionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }   
}
