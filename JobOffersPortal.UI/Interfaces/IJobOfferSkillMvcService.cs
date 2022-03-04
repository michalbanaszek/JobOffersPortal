using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.ViewModels.JobOfferSkillMvc.DetailJobOfferSkillMvc;
using WebApp.ViewModels.JobOfferSkillMvc.IndexJobOfferSkillMvc;

namespace WebApp.Interfaces
{
    public interface IJobOfferSkillMvcService
    {
        Task<List<JobOfferSkillMvcViewModel>> GetAllAsync(string jobOfferId);
        Task<JobOfferSkillDetailMvcViewModel> GetByIdAsync(string id);
        Task<ResponseFromApi<string>> AddAsync(string jobOfferId, string content);
        Task<ResponseFromApi<string>> UpdateAsync(string id, string content);
        Task<ResponseFromApi<string>> DeleteAsync(string id);
    }
}
