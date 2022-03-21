using MediatR;

namespace JobOffersPortal.Application.Functions.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetailsViewModel>
    {
        public string Id { get; set; }
    }
}
