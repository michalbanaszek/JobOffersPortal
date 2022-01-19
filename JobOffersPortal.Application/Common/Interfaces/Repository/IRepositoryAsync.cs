using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Interfaces.Persistance
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
