using System;

namespace JobOffersPortal.UI.ViewModels.JobOfferMvc.DeleteJobOfferMvc
{
    public class JobOfferDeleteMvcViewModel
    {
        public string Id { get; set; }

        public string Position { get; set; }

        public string Salary { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }
    }
}
