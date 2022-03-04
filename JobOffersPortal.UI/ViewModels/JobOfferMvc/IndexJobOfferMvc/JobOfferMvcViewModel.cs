using System;
using System.Collections.Generic;

namespace WebApp.ViewModels.JobOfferMvc.IndexJobOfferMvc
{
    public class JobOfferMvcViewModel
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string Position { get; set; }

        public string Salary { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        public List<JobOfferRequirementMvcDto> Requirements { get; set; } = new List<JobOfferRequirementMvcDto>();

        public List<JobOfferSkillMvcDto> Skills { get; set; } = new List<JobOfferSkillMvcDto>();

        public List<JobOfferPropositionMvcDto> Propositions { get; set; } = new List<JobOfferPropositionMvcDto>();
    }
}