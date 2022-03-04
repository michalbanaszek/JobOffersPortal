using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.ViewModels.JobOfferMvc.CreateJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.IndexJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.UpdateJobOfferMvc;

namespace WebApp.Interfaces
{
    public interface IJobOfferMvcService
    {
        Task<PaginatedMvcList<JobOfferMvcViewModel>> GetAllByCompany(string companyId);
        Task<JobOfferMvcViewModel> GetByIdAsync(string jobOfferId);       
        Task<ResponseFromApi<string>> AddAsync(CreateJobOfferMvcViewModel createJobOfferViewModel);
        Task<ResponseFromApi<string>> UpdateAsync(string jobOfferId, UpdateJobOfferMvcViewModel updateJobOfferViewModel);
        Task<ResponseFromApi<string>> DeleteAsync(string jobOfferId);
    }
}
