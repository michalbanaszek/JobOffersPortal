using JobOffersPortal.Application.Common.Options;
using JobOffersPortal.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryAsync<Company>
    {
        IQueryable<Company> GetAllCompaniesIncludeEntitiesWithOptions(SearchJobOfferOptions searchJobOfferOptions);
        Task<Company> GetByIdIncludeEntitiesAsync(string id);
        Task<bool> IsNameAlreadyExistAsync(string name);
        Task<bool> UserOwnsEntityAsync(string id, string userId);
    }
}
