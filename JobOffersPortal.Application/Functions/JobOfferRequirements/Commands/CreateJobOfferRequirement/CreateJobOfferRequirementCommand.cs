using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement
{
    public class CreateJobOfferRequirementCommand : IRequest<CreateJobOfferRequirementResponse>
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }
    }


}
