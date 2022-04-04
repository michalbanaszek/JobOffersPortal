using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.CreateDetailsJobOfferRequirementMvc
{
    public class CreateDetailsJobOfferRequirementMvcViewModel
    {
        public string JobOfferId { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Content { get; set; }
        public List<JobOfferJobOfferRequirementMvcDto> Requirements { get; set; }
    }
}
