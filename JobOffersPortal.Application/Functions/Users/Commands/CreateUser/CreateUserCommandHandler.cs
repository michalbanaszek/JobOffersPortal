using JobOffersPortal.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IUriService _uriService;

        public CreateUserCommandHandler(IUserService userService, ILogger<CreateUserCommandHandler> logger, IUriService uriService)
        {
            _userService = userService;
            _logger = logger;
            _uriService = uriService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var id = await _userService.CreateUserAsync(command.Email, command.Password);

            var uri = _uriService.Get(id, "User");           

            return new CreateUserCommandResponse(uri);
        }
    }
}
