using System;
using System.Collections.Generic;

namespace JobOffersPortal.WebUI.Domain
{
    public class JobOffer : BaseModel
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
