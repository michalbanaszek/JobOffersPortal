using Application.JobOffers.Queries.GetJobOffer;
using System.Collections.Generic;

namespace Application.JobOffers.Queries.GetListJobOffers
{
    public class JobOfferIndexViewModel
    {
        public JobOfferIndexViewModel()
        {
            Items = new List<JobOfferViewModel>();
        }

        public List<JobOfferViewModel> Items { get; set; }
    }
}
