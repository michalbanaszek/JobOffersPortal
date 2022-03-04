using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.JobOfferRequirementMvc.CreateDetailsJobOfferRequirrementMvc
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
