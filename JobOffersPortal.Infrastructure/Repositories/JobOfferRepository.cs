using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using JobOffersPortal.Persistance.EF.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersPortal.Persistance.EF.Repositories
{
    public class JobOfferRepository : BaseRepository<JobOffer>, IJobOfferRepository
    {
        public JobOfferRepository(ApplicationDbContext context) : base(context)
        {}

        public IQueryable<JobOffer> GetAllByCategory(string categoryId)
        {
            return _context.JobOffers
                                  .Include(x => x.Requirements)
                                  .Include(x => x.Skills)
                                  .Include(x => x.Propositions)
                                  .Where(x => x.CompanyId == categoryId)
                                  .OrderBy(x => !x.IsAvailable);
        }

        public async Task<List<JobOffer>> GetAllIncludeAllEntities()
        {
            return await _context.JobOffers
                                       .Include(x => x.Requirements)
                                       .Include(x => x.Skills)
                                       .Include(x => x.Propositions)
                                      .ToListAsync();
        }

        public async Task<JobOffer> GetByIdIncludeAllEntities(string id)
        {
            return await _context.JobOffers
                                      .Include(x => x.Requirements)
                                      .Include(x => x.Skills)
                                      .Include(x => x.Propositions)
                                      .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UserOwnsEntityAsync(string id, string userId)
        {
            var entity = await _context.JobOffers.AsNoTracking()
                                                 .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            return entity.CreatedBy != userId ? false : true;
        }
    }
}
