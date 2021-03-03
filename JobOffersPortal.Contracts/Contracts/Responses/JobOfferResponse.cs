using System;

namespace JobOffersPortal.WebUI.Contracts.Responses
{
    public class JobOfferResponse
    {
        public string Id { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public DateTime Date { get; set; }
        public string Requirements { get; set; }
        public string Skills { get; set; }
        public string Offers { get; set; }
    }
}
