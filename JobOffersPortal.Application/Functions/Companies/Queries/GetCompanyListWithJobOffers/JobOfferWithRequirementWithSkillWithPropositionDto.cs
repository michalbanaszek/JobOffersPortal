using System;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyListWithJobOffers
{
    public class JobOfferWithRequirementWithSkillWithPropositionDto
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Position { get; set; }
        public string Salary { get; set; }
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }
        public List<JobOfferWithPropositionDto> Propositions { get; set; }
        public List<JobOfferWithRequirementDto> Requirements { get; set; }
        public List<JobOfferWithSkillDto> Skills { get; set; }
    }
}
