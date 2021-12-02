using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyListWithJobOffers
{
    public class CompanyJobOfferListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<JobOfferWithRequirementWithSkillWithPropositionDto> JobOffers { get; set; }
    }
}
