namespace JobOffersPortal.Domain.Entities
{
    public class JobOfferProposition
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public JobOffer JobOffer { get; set; }
        public string JobOfferId { get; set; }
    }
}
