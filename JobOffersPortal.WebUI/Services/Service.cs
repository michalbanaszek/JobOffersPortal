using JobOffersPortal.WebUI.Data;
using JobOffersPortal.WebUI.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Services
{
    public class Service<T> : IService<T> where T : BaseModel
    {
        private readonly DataContext _context;

        public Service(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);

            var added = await _context.SaveChangesAsync();

            return added > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            _context.Set<T>().Remove(entity);

            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<List<T>> GetAllAsync(GetAllFilter filter = null, PaginationFilter paginationFilter = null)
        {
            var queryable = _context.Set<T>().AsQueryable();

            if (paginationFilter == null)
            {
                return await queryable.ToListAsync();
            }

            queryable = AddFilterOnQuery(filter, queryable);

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            return await queryable.OrderBy(x => x.UserId)
                                  .Skip(skip)
                                  .Take(paginationFilter.PageSize)
                                  .ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entityToUpdate)
        {
            _context.Set<T>().Update(entityToUpdate);

            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        private static IQueryable<T> AddFilterOnQuery(GetAllFilter filter, IQueryable<T> queryable)
        {
            if (!string.IsNullOrEmpty(filter?.UserId))
            {
               queryable = queryable.Where(x => x.UserId == filter.UserId);
            }

            return queryable;
        }
    }
}
