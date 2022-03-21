using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsViewModel>
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetUserDetailsQueryHandler> _logger;

        public GetUserDetailsQueryHandler(IUserService userService, ILogger<GetUserDetailsQueryHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<UserDetailsViewModel> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var userResult = await _userService.GetUserByIdAsync(request.Id);

            if (userResult == null)
            {
                _logger.LogWarning("User Id not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException("User", request.Id);
            }

            return new UserDetailsViewModel() { Id = userResult.Id, Email = userResult.Email };
        }
    }
}
