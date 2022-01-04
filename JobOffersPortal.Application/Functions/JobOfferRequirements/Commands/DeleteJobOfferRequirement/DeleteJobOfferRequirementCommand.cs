using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
using MediatR;

namespace Application.JobOfferRequirements.Commands.DeleteJobOfferRequirement
{
    public class DeleteJobOfferRequirementCommand : IRequest<DeleteJobOfferRequirementCommandResponse>
    {
        public string Id { get; set; }
    }
}
