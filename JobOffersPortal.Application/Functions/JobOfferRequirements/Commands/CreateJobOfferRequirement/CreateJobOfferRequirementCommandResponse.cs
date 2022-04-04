using System;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement
{
    public class CreateJobOfferRequirementCommandResponse
    {
        public Uri Uri { get; set; }

        public CreateJobOfferRequirementCommandResponse(Uri uri)
        {
            Uri = uri;
        }
    }
}
