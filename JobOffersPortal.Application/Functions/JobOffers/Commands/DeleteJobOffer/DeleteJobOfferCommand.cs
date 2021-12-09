using MediatR;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer
{
    public class DeleteJobOfferCommand : IRequest<DeleteJobOfferCommandResponse>
    {
        public string Id { get; set; }
    }
}
