using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillList
{
    public class JobOfferSkillViewModel
    {
        public string Id { get; set; }

        public List<JobOfferJobOfferSkillDto> Skills { get; set; }
    }
}
