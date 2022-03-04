using System;

namespace WebApp.ViewModels.JobOfferMvc.DetailJobOfferMvc
{
    public class JobOfferDetailMvcViewModel
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }

        public string Position { get; set; }

        public string Salary { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }
    }
}
