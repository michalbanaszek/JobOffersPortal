using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition
{
    public class DeleteJobOfferPropositionCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
