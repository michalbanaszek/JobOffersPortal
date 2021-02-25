using JobOffersPortal.Contracts.Contracts.Requests;
using JobOffersPortal.WebUI.Contracts.Requests;
using JobOffersPortal.WebUI.Contracts.Responses;
using JobOffersPortal.WebUI.Domain;
using JobOffersPortal.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Controllers
{   
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Register user in the system
        /// </summary>
        /// <response code="200">Creates user in the system</response>
        /// <response code="400">Unable to create the user due to validation error</response> 
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
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
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
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
        [HttpPost(ApiRoutes.Identity.FacebookAuth)]
        public async Task<IActionResult> FacebookAuth([FromBody] LoginFacebookRequest request)
        {
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
        [HttpPost(ApiRoutes.Identity.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            return CheckAuthenticationResult(authResponse);
        }

        private IActionResult CheckAuthenticationResult(AuthenticationResult authResponse)
        {
            if (!authResponse.Success)
            {
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
