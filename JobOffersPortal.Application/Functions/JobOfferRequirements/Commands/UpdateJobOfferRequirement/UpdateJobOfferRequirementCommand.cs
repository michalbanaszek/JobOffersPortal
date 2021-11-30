using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement
{
    public class UpdateJobOfferRequirementCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }
}
