using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;


namespace Application.AccountLdap.Commands
{
    public class LoginLdapCommand : IRequest<AuthenticationLdapResult>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LoginLdapCommandHandler : IRequestHandler<LoginLdapCommand, AuthenticationLdapResult>
    {
        private readonly IAuthenticationService _authService;
        private readonly ILogger<LoginLdapCommandHandler> _logger;

        public LoginLdapCommandHandler(IAuthenticationService authService, ILogger<LoginLdapCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public Task<AuthenticationLdapResult> Handle(LoginLdapCommand request, CancellationToken cancellationToken)
        {
            var authResponse = _authService.Login(request.Username, request.Password);

            // If the user is authenticated, store its claims to cookie
            if (authResponse != null && authResponse.Success)
            {
                _logger.LogInformation("Logged succeeded, Login: {0}", authResponse.User.Username);

                var userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, authResponse.User.Username),
                            new Claim(ClaimTypes.Email, authResponse.User.Email)
                        };

                // Roles
                foreach (var role in authResponse.User.Roles)
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                //we can add custom claims based on the AD user's groups
                var claimsIdentity = new ClaimsIdentity(userClaims, _authService.GetType().Name);

                if (Array.Exists(authResponse.User.Roles, s => s.Contains("Jobs_App")))
                {
                    //if in the AD the user belongs to the aspnetcore.ldap group, we add a claim
                    claimsIdentity.AddClaim(new Claim("Read", "true"));
                }

                authResponse.ClaimsIdentity = claimsIdentity;

                return Task.FromResult(authResponse);
            }

            foreach (var error in authResponse.Errors)
            {
                _logger.LogWarning(error);
            }

            return Task.FromResult(authResponse);
        }
    }
}