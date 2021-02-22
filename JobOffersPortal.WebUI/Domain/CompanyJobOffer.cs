using System.ComponentModel.DataAnnotations.Schema;

namespace JobOffersPortal.WebUI.Domain
{
    public class CompanyJobOffer
    {      
        public string CompanyId { get; set; }
        public string JobOfferId { get; set; }

        public virtual Company Company { get; set; }

        [ForeignKey(nameof(JobOfferId))]
        public virtual JobOffer JobOffer { get; set; }

    }
}
