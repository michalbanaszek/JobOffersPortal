﻿using System;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionCommandResponse
    {
        public Uri Uri { get; set; }

        public CreateJobOfferPropositionCommandResponse(Uri uri)
        {
            Uri = uri;
        }
    }
}