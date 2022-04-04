using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.DetailJobOfferPropositionMvc;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.IndexJobOfferPropositionMvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.Interfaces
{
    public interface IJobOfferPropositionMvcService
    {
        Task<List<JobOfferPropositionMvcViewModel>> GetAllAsync(string jobOfferId);
        Task<JobOfferPropositionDetailMvcViewModel> GetByIdAsync(string id);
        Task AddAsync(string jobOfferId, string content);
        Task UpdateAsync(string id, string content);
        Task DeleteAsync(string id);
    }
}
