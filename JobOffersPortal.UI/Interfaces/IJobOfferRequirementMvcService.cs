using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.DetailJobOfferRequirementMvc;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.IndexJobOfferRequirementMvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.Interfaces
{
    public interface IJobOfferRequirementMvcService
    {
        Task<List<JobOfferRequirementMvcViewModel>> GetAllAsync(string jobOfferId);
        Task<JobOfferRequirementDetailMvcViewModel> GetByIdAsync(string id);
        Task AddAsync(string jobOfferId, string content);
        Task UpdateAsync(string id, string content);
        Task DeleteAsync(string id);
    }
}
