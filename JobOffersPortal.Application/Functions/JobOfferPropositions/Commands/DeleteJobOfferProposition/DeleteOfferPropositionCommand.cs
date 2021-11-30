using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition
{
    public class DeleteOfferPropositionCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
