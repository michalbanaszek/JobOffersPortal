using System.Collections.Generic;

namespace JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.CreateDetailsJobOfferSkillMvc
{
    public class CreateDetailsJobOfferSkillMvcViewModel
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }
        public List<JobOfferJobOfferSkillMvcDto> Skills { get; set; }
    }
}
