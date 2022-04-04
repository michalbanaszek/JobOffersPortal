using MediatR;

namespace JobOffersPortal.Application.Functions.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
