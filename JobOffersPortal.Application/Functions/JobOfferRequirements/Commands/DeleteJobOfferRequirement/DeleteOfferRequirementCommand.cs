using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
using MediatR;

namespace Application.JobOfferRequirements.Commands.DeleteJobOfferRequirement
{
    public class DeleteOfferRequirementCommand : IRequest<DeleteOfferRequirementCommandResponse>
    {
        public string Id { get; set; }
    }
}
