using Application.Common.Models;
using Application.Identity.Commands;
using Application.Identity.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI;
using WebUI.Controllers;

namespace JobOffersPortal.WebUI.Controllers
{
    public class IdentityController : ApiControllerBase
    {
        /// <summary>
        /// Register user in the system
        /// </summary>
        /// <response code="200">Creates user in the system</response>
        /// <response code="400">Unable to create the user due to validation error</response> 
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        [HttpPost(ApiRoutes.IdentityRoute.Register), AllowAnonymous]      
        public async Task<ActionResult<AuthSuccessResponse>> Register([FromBody] RegisterCommand command)
        {
           var authResponse = await Mediator.Send(command);

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
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var authResponse = await Mediator.Send(command);

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
        public async Task<IActionResult> FacebookAuth([FromBody] LoginFacebookCommand command)
        {
            var authResponse = await Mediator.Send(command);

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
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var authResponse = await Mediator.Send(command);

            return CheckAuthenticationResult(authResponse);
        }

        private ActionResult CheckAuthenticationResult(AuthenticationResult authResponse)
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
