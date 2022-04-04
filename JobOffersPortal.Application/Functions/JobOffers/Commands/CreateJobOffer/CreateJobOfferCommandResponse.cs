using System;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer
{
    public class CreateJobOfferCommandResponse
    {
        public CreateJobOfferCommandResponse(Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; set; }
    }
}
