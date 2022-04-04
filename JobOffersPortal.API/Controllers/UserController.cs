using JobOffersPortal.Application;
using JobOffersPortal.Application.Functions.Users.Commands.CreateUser;
using JobOffersPortal.Application.Functions.Users.Commands.DeleteUser;
using JobOffersPortal.Application.Functions.Users.Queries.GetUserDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserController : ApiControllerBase
    {
        /// <summary>
        /// Get user from the system
        /// </summary>
        /// <response code="200">Get user from the system</response>
        /// <response code="401">Unauthorized</response>   
        /// <response code="404">Not found user</response> 
        [HttpGet(ApiRoutes.UserRoute.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsViewModel>> Get([FromRoute] string userId)
        {           
            return Ok(await Mediator.Send(new GetUserDetailsQuery() { Id = userId }));
        }

        /// <summary>
        /// Create user to the system
        /// </summary>
        /// <response code="201">Creates user to the system</response>
        /// <response code="400">Unable to create user</response> 
        /// <response code="401">Unauthorized</response>   
        [HttpPost(ApiRoutes.UserRoute.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Create([FromBody] CreateUserCommand command)
        {
            var response = await Mediator.Send(command);

            return Created(response.Uri, null);
        }

        /// <summary>
        /// Delete user to the system
        /// </summary>
        /// <response code="204">Delete user from the system</response>
        /// <response code="400">Unable to create user</response> 
        /// <response code="401">Unauthorized</response>   
        /// <response code="404">Not found user</response> 
        [HttpDelete(ApiRoutes.UserRoute.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] string userId)
        {
            await Mediator.Send(new DeleteUserCommand() { Id = userId });

            return NoContent();
        }
    }
}