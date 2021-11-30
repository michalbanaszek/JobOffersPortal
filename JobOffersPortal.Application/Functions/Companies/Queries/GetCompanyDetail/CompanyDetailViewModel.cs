using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail
{
    public class CompanyDetailViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IList<JobOfferDto> JobOffers { get; set; } = new List<JobOfferDto>();
    }
}
