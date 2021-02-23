using System.Collections.Generic;

namespace JobOffersPortal.WebUI.Domain
{
    public class Company : BaseModel
    {
        public Company()
        {
            JobOffers = new List<CompanyJobOffer>();
        }

        public string Id { get; set; }
        public string Name { get; set; }      

        //public string City { get; set; }

        //public string AboutUs { get; set; }

        public virtual List<CompanyJobOffer> JobOffers { get; set; }

    }
}
