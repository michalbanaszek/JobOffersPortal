using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementList
{
    public class JobOfferRequirementViewModel
    {
        public string Id { get; set; }

        public List<JobOfferJobOfferRequirementDto> Requirements { get; set; }
    }
}
