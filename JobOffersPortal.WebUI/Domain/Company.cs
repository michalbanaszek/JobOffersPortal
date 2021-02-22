using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobOffersPortal.WebUI.Domain
{
    public class Company : BaseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }      

        //public string City { get; set; }
        //public string AboutUs { get; set; }

        public virtual List<CompanyJobOffer> JobOffers { get; set; }

    }
}
