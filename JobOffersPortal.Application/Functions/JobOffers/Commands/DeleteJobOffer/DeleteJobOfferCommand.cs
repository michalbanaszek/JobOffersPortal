using MediatR;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer
{
    public class DeleteJobOfferCommand : IRequest
    {
        public string Id { get; set; }
    }
}
