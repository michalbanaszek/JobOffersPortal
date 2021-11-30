namespace Domain.Entities
{
    public class JobOfferSkill
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public JobOffer JobOffer { get; set; }
    }
}
