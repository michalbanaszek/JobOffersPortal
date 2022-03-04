using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.ViewModels.JobOfferPropositionMvc.DetailJobOfferPropositionMvc;
using WebApp.ViewModels.JobOfferPropositionMvc.IndexJobOfferPropositionMvc;

namespace WebApp.Interfaces
{
    public interface IJobOfferPropositionMvcService
    {
        Task<List<JobOfferPropositionMvcViewModel>> GetAllAsync(string jobOfferId);
        Task<JobOfferPropositionDetailMvcViewModel> GetByIdAsync(string id);
        Task<ResponseFromApi<string>> AddAsync(string jobOfferId, string content);
        Task<ResponseFromApi<string>> UpdateAsync(string id, string content);
        Task<ResponseFromApi<string>> DeleteAsync(string id);
    }
}
