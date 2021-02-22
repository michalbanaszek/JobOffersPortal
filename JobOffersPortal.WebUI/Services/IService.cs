using JobOffersPortal.WebUI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Services
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAllAsync(GetAllFilter filter = null, PaginationFilter paginationFilter = null);
        Task<T> GetByIdAsync(string id);
        Task<bool> UpdateAsync(T entityToUpdate);
        Task<bool> DeleteAsync(string id);
        Task<bool> CreateAsync(T entity);      
    }
}
