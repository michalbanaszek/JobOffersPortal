using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionList
{
    public class JobOfferPropositionViewModel
    {
        public string Id { get; set; }

        public List<JobOfferJobOfferPropositionDto> Propositions { get; set; }
    }
}
