using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JobOfferService : IJobOfferService
    {
        private readonly ApplicationDbContext _context;

        public JobOfferService(ApplicationDbContext context)
        {
            _context = context;
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
