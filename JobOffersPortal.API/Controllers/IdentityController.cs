using JobOffersPortal.Application;
using JobOffersPortal.Application.Security.Contracts;
using JobOffersPortal.Application.Security.Models.AuthResult;
using JobOffersPortal.Application.Security.Models.External;
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

        public IdentityController(IIdentityService identityService, ILogger<IdentityController> logger)
        {
            _identityService = identityService;
            _logger = logger;
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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
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
        public async Task<IActionResult> FacebookAuth([FromBody] LoginFacebookRequest request)
        {
            _logger.LogInformation("Invoked FacebookAuth endpoint");

            var authResponse = await _identityService.LoginWithFacebookAsync(request.TokenAccess);

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
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
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
