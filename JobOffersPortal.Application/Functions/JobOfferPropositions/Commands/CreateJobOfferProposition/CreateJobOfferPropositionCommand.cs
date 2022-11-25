using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public sealed class CreateJobOfferPropositionCommand : IRequest<CreateJobOfferPropositionCommandResponse>
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }
    }

}
