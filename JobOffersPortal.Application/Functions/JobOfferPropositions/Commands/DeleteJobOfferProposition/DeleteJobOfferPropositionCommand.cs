using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition
{
    public class DeleteJobOfferPropositionCommand : IRequest<DeleteJobOfferPropositionCommandResponse>
    {
        public string Id { get; set; }
    }
}
