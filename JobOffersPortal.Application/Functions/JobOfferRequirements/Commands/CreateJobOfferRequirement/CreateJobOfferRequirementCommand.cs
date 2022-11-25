using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement
{
    public sealed class CreateJobOfferRequirementCommand : IRequest<CreateJobOfferRequirementCommandResponse>
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }
    }


}
