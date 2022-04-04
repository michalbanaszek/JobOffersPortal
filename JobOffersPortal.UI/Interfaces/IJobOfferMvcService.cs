using JobOffersPortal.UI.ViewModels.JobOfferMvc.CreateJobOfferMvc;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.IndexJobOfferMvc;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.UpdateJobOfferMvc;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.Interfaces
{
    public interface IJobOfferMvcService
    {
        Task<PaginatedMvcList<JobOfferMvcViewModel>> GetAllByCompany(string companyId);
        Task<JobOfferMvcViewModel> GetByIdAsync(string jobOfferId);
        Task AddAsync(CreateJobOfferMvcViewModel createJobOfferViewModel);
        Task UpdateAsync(string jobOfferId, UpdateJobOfferMvcViewModel updateJobOfferViewModel);
        Task DeleteAsync(string jobOfferId);
    }
}
