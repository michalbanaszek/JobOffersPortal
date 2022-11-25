using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserService userService, ILogger<DeleteUserCommandHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _userService.GetUserByIdAsync(request.Id);

            if (userResult is null)
            {
                _logger.LogWarning("User Id not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException("User", request.Id);
            }

            await _userService.DeleteUserAsync(userResult.Id);

            _logger.LogInformation("Deleted User Id: {0}", userResult.Id);

            return Unit.Value;
        }
    }
}
