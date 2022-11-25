using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition
{
    public sealed class DeleteJobOfferPropositionCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
