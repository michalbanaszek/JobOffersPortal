using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.ViewModels.JobOfferRequirementMvc.DetailJobOfferRequirementMvc;
using WebApp.ViewModels.JobOfferRequirementMvc.IndexJobOfferRequirementMvc;

namespace WebApp.Interfaces
{
    public interface IJobOfferRequirementMvcService
    {
        Task<List<JobOfferRequirementMvcViewModel>> GetAllAsync(string jobOfferId);
        Task<JobOfferRequirementDetailMvcViewModel> GetByIdAsync(string id);
        Task<ResponseFromApi<string>> AddAsync(string jobOfferId, string content);
        Task<ResponseFromApi<string>> UpdateAsync(string id, string content);
        Task<ResponseFromApi<string>> DeleteAsync(string id);
    }
}
