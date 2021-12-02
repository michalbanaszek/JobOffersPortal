using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Interfaces.Persistance
{
    public interface IJobOfferRepository : IRepositoryAsync<JobOffer>
    {
        Task<bool> UserOwnsEntityAsync(string id, string userId);
        Task<List<JobOffer>> GetAllIncludeAllEntities();
        Task<JobOffer> GetByIdIncludeAllEntities(string id);
        IQueryable<JobOffer> GetAllByCategory(string categoryId);
    }
}
