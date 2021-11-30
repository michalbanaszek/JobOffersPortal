using Application.JobOfferPropositions.Commands.CreateJobOfferProposition;
using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionCommand : IRequest<CreateJobOfferPropositionResponse>
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }
    }

}
