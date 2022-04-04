using System;

namespace JobOffersPortal.UI.ViewModels.JobOfferMvc.UpdateJobOfferMvc
{
    public class UpdateJobOfferMvcViewModel
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Position { get; set; }
        public string Salary { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsAvailable { get; set; }
    }
}