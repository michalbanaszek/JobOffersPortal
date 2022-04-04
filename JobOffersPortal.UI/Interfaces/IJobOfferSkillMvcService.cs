using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.DetailJobOfferSkillMvc;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.IndexJobOfferSkillMvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.Interfaces
{
    public interface IJobOfferSkillMvcService
    {
        Task<List<JobOfferSkillMvcViewModel>> GetAllAsync(string jobOfferId);
        Task<JobOfferSkillDetailMvcViewModel> GetByIdAsync(string id);
        Task AddAsync(string jobOfferId, string content);
        Task UpdateAsync(string id, string content);
        Task DeleteAsync(string id);
    }
}
