using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using JobOffersPortal.Persistance.EF.Persistence;

namespace JobOffersPortal.Persistance.EF.Repositories
{
    public class JobOfferSkillRepository : BaseRepository<JobOfferSkill>, IJobOfferSkillRepository
    {
        public JobOfferSkillRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
