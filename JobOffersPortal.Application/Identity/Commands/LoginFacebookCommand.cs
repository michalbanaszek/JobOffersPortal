using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands
{
    public class LoginFacebookCommand : IRequest<AuthenticationResult>
    {
        public string TokenAccess { get; set; }
    }

    public class LoginFacebookCommandHandler : IRequestHandler<LoginFacebookCommand, AuthenticationResult>
    {
        private readonly IIdentityService _identityService;

        public LoginFacebookCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthenticationResult> Handle(LoginFacebookCommand request, CancellationToken cancellationToken)
        {
            var authResponse = await _identityService.LoginWithFacebookAsync(request.TokenAccess);
            return authResponse;
        }
    }
}
