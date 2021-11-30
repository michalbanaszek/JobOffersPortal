using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyListWithJobOffers;
using System;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList
{
    public class JobOfferWithRequirementWithSkillWithPropositionDto
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Position { get; set; }
        public string Salary { get; set; }
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }
        public JobOfferWithPropositionDto Proposition { get; set; }
        public JobOfferWithRequirementDto Requirement { get; set; }
        public JobOfferWithSkillDto Skill { get; set; }
    }
}
