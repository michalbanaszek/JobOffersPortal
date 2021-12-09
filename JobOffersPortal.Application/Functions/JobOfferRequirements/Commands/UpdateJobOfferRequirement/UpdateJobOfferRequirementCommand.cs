using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement
{
    public class UpdateJobOfferRequirementCommand : IRequest<UpdateJobOfferRequirementCommandResponse>
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }
}
