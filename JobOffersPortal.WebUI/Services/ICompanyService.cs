using JobOffersPortal.WebUI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Services
{
    public interface ICompanyService : IService<Company>
    {
        Task<List<Company>> GetAllIncludedAsync(GetAllFilter query = null, PaginationFilter paginationFilter = null);
        Task<Company> GetByIdIncludedAsync(string id);
        Task<bool> UserOwnsEntityAsync(string id, string userId);
    }
}
