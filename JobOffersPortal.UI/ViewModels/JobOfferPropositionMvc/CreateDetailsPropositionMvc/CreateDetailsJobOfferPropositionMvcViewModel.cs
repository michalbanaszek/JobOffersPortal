﻿using System.Collections.Generic;

namespace JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.CreateDetailsPropositionMvc
{
    public class CreateDetailsJobOfferPropositionMvcViewModel
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }
        public List<JobOfferJobOfferPropositionMvcDto> Propositions { get; set; }
    }
}
