using System.Collections.Generic;

namespace JobOffersPortal.WebUI.Contracts.Requests
{
    public class CreateCompanyRequest
    {
        public CreateCompanyRequest()
        {
            JobOffers = new List<string>();
        }

        public string Name { get; set; }

        public IEnumerable<string> JobOffers { get; set; }

      
    }
}
