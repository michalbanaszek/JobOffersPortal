using JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer;
using MediatR;

namespace JobOffersPortal.Application.Functions.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<DeleteJobOfferCommandResponse>
    {
        public string Id { get; set; }
    }
}
