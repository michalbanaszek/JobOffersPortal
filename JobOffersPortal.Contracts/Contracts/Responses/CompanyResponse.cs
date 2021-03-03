using JobOffersPortal.Contracts.Contracts.Responses;
using System.Collections.Generic;

namespace JobOffersPortal.WebUI.Contracts.Responses
{
    public class CompanyResponse
    {
        public CompanyResponse()
        {
            JobOffers = new List<CompanyJobOfferResponse>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public IEnumerable<CompanyJobOfferResponse> JobOffers { get; set; }
    }
}
