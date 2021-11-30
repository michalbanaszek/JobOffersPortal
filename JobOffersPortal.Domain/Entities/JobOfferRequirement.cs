namespace Domain.Entities
{
    public class JobOfferRequirement
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public JobOffer JobOffer { get; set; }
    }
}
