using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement
{
    public sealed class DeleteJobOfferRequirementCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
