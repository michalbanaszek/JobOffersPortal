using JobOffersPortal.Application.Common.Enums;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using JobOffersPortal.Persistance.EF.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersPortal.Persistance.EF.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Company> GetAllCompaniesIncludeEntitiesWithOptions(SearchCompanyOptions searchJobOfferOptions)
        {
            switch (searchJobOfferOptions)
            {
                case SearchCompanyOptions.All:
                    return _context.Companies
                                   .Include(x => x.JobOffers)
                                       .ThenInclude(x => x.Requirements)
                                   .Include(x => x.JobOffers)
                                       .ThenInclude(x => x.Skills)
                                   .Include(x => x.JobOffers)
                                       .ThenInclude(x => x.Propositions);


                case SearchCompanyOptions.SortByName:
                    return _context.Companies.Include(x => x.JobOffers)
                                                .ThenInclude(x => x.Propositions)
                                             .Include(x => x.JobOffers)
                                                .ThenInclude(x => x.Requirements)
                                             .Include(x => x.JobOffers)
                                                .ThenInclude(x => x.Skills)
                                             .OrderBy(x => x.Name);
                default:
                    return _context.Companies;
            }
        }

        public Task<Company> GetByIdIncludeEntitiesAsync(string id)
        {
            return _context.Companies.Include(x => x.JobOffers).Include(x => x.JobOffers)
                                                                  .ThenInclude(x => x.Propositions)
                                                               .Include(x => x.JobOffers)
                                                                  .ThenInclude(x => x.Requirements)
                                                               .Include(x => x.JobOffers)
                                                                  .ThenInclude(x => x.Skills)
                                                               .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsNameAlreadyExistAsync(string name)
        {
            return await _context.Companies.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> UserOwnsEntityAsync(string id, string userId)
        {
            var entity = await _context.Companies.AsNoTracking()
                                                 .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            return entity.CreatedBy != userId ? false : true;
        }
    }
}
