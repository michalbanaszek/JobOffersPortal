using System;

namespace JobOffersPortal.WebUI.Contracts.Requests
{
    public class CreateJobOfferRequest
    {
        public string Position { get; set; }
        public int Salary { get; set; }
        public DateTime Date { get; set; }
        public string Requirements { get; set; }
        public string Skills { get; set; }
        public string Offers { get; set; }
    }
}
