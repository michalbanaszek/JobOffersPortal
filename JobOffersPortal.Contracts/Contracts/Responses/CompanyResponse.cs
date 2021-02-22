using System.Collections.Generic;

namespace JobOffersPortal.WebUI.Contracts.Responses
{
    public class CompanyResponse
    {
        public CompanyResponse()
        {
            JobOffers = new List<JobOfferResponse>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public IEnumerable<JobOfferResponse> JobOffers { get; set; }
    }
}
