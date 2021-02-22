using JobOffersPortal.WebUI.Data;
using JobOffersPortal.WebUI.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Services
{
    public class CompanyService : Service<Company>, ICompanyService
    {
        private readonly DataContext _context;

        public CompanyService(DataContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<Company>> GetAllIncludedAsync(GetAllFilter filter = null, PaginationFilter paginationFilter = null)
        {
            var queryable = _context.Companies.AsQueryable();

            if (paginationFilter == null)
            {
                return await queryable.Include(x => x.JobOffers)
                                      .ThenInclude(x => x.JobOffer)
                                      .ToListAsync();
            }

            queryable = AddFilterOnQuery(filter, queryable);

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            return await queryable.Include(x => x.JobOffers)
                                  .ThenInclude(x => x.JobOffer)
                                  .OrderBy(x => x.Name)
                                  .Skip(skip)
                                  .Take(paginationFilter.PageSize)
                                  .ToListAsync();
        }

        public async Task<Company> GetByIdIncludedAsync(string id)
        {
            return await _context.Companies.Include(x => x.JobOffers)
                                           .ThenInclude(x => x.JobOffer)
                                           .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UserOwnsEntityAsync(string id, string userId)
        {
            var entity = await _context.Companies.AsNoTracking()
                                                 .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            return entity.UserId != userId ? false : true;
        }

        private static IQueryable<Company> AddFilterOnQuery(GetAllFilter filter, IQueryable<Company> queryable)
        {
            if (!string.IsNullOrEmpty(filter?.UserId))
            {
                queryable = queryable.Where(x => x.UserId == filter.UserId);
            }

            return queryable;
        }
    }
}
