using System;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail
{
    public class JobOfferDto
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string Position { get; set; }

        public string Salary { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        public List<JobOfferRequirementDto> Requirements { get; set; } = new List<JobOfferRequirementDto>();

        public List<JobOfferSkillDto> Skills { get; set; } = new List<JobOfferSkillDto>();

        public List<JobOfferPropositionDto> Propositions { get; set; } = new List<JobOfferPropositionDto>();
    }
}
