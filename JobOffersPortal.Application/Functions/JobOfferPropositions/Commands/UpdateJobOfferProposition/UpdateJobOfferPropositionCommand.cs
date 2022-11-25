using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition
{
    public sealed class UpdateJobOfferPropositionCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }


}
