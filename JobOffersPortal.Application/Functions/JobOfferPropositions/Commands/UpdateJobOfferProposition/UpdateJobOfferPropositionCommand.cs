using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition
{
    public class UpdateJobOfferPropositionCommand : IRequest<UpdateJobOfferPropositionCommandResponse>
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }


}
