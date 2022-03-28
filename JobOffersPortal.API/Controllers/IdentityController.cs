using App.Metrics;
using App.Metrics.Counter;
using JobOffersPortal.API.Metrics;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Security.Services;
using JobOffersPortal.Domain.Entities;
using JobOffersPortal.Infrastructure.Security.Contracts.Identity.Requests;
using JobOffersPortal.Infrastructure.Security.Contracts.Identity.Responses;
using JobOffersPortal.Infrastructure.Security.Contracts.OAuth.Facebook.Requests;
using JobOffersPortal.Infrastructure.Security.Contracts.OAuth.Ldap.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<IdentityController> _logger;
        private readonly IMetrics _metrics;

        public IdentityController(IIdentityService identityService, ILogger<IdentityController> logger, IMetrics metrics)
        {
            _identityService = identityService;
            _logger = logger;
            _metrics = metrics;
        }

        /// <summary>
        /// Register user in the system
        /// </summary>
        /// <response code="200">Creates user in the system</response>
        /// <response code="400">Unable to create the user due to validation error</response> 
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        [HttpPost(ApiRoutes.IdentityRoute.Register), AllowAnonymous]
        public async Task<ActionResult<AuthSuccessResponse>> Register([FromBody] RegisterRequest request)
        {
            _logger.LogInformation("Invoked Register endpoint");

            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            _metrics.Measure.Counter.Increment(MericsRegistry.CreatedCustomerCounter);

            return CheckAuthenticationResult(authResponse);
        }

        /// <summary>
        /// Login user in the system
        /// </summary>
        /// <response code="200">Login user in the system</response>
        /// <response code="400">Unable to login the user due to validation error</response>   
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        [HttpPost(ApiRoutes.IdentityRoute.Login), AllowAnonymous]
        public async Task<ActionResult<AuthSuccessResponse>> Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Invoked Login endpoint");

            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            return CheckAuthenticationResult(authResponse);
        }

        /// <summary>
        /// Login facebook user in the system
        /// </summary>
        /// <response code="200">Login facebook user in the system</response>
        /// <response code="400">Unable to login the user due to validation error</response>   
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        [HttpPost(ApiRoutes.IdentityRoute.FacebookAuth), AllowAnonymous]
        public async Task<ActionResult<AuthSuccessResponse>> FacebookAuth([FromBody] LoginFacebookRequest request)
        {
            _logger.LogInformation("Invoked FacebookAuth endpoint");

            var authResponse = await _identityService.LoginFacebookAsync(request.TokenAccess);

            return CheckAuthenticationResult(authResponse);
        }

        /// <summary>
        /// Login ldap user 
        /// </summary>
        /// <response code="200">Login ldap user</response>
        /// <response code="400">Unable to login the user due to validation error</response>   
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        [HttpPost(ApiRoutes.IdentityRoute.LdapAuth), AllowAnonymous]
        public async Task<ActionResult<AuthSuccessResponse>> LdapAuth([FromBody] LoginLdapRequest request)
        {
            _logger.LogInformation("Invoked LdapAuth endpoint");

            var authResponse = await _identityService.LoginLdap(request.Email, request.Password);

            return CheckAuthenticationResult(authResponse);
        }


        /// <summary>
        /// Generate refresh token for user in the system
        /// </summary>
        /// <response code="200">Generate refresh token in the system</response>
        /// <response code="400">Unable to generate refresh token due to validation error</response>   
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        [HttpPost(ApiRoutes.IdentityRoute.RefreshToken), AllowAnonymous]
        public async Task<ActionResult<AuthSuccessResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            _logger.LogInformation("Invoked RefreshToken endpoint");

            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            return CheckAuthenticationResult(authResponse);
        }

        private ActionResult CheckAuthenticationResult(AuthenticationResult authResponse)
        {
            if (!authResponse.Success)
            {
                foreach (var error in authResponse.Errors)
                {
                    _logger.LogError(error);
                }

                return BadRequest(new AuthFailedResponse()
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse()
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}
