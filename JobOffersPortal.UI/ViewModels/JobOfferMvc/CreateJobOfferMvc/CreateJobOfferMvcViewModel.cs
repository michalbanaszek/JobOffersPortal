using System;

namespace WebApp.ViewModels.JobOfferMvc.CreateJobOfferMvc
{
    public class CreateJobOfferMvcViewModel
    {
        public string CompanyId { get; set; }

        public string Position { get; set; }
     
        public string Salary { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public bool IsAvailable { get; set; }
    }
}
