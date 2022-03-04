using JobOffersPortal.Application;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Infrastructure.Security.Contracts.User.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Get user from the system
        /// </summary>
        /// <response code="200">Get user from the system</response>
        /// <response code="404">Not found user</response> 
        [HttpGet(ApiRoutes.UserRoute.Get)]
        public async Task<ActionResult<string>> Get([FromRoute] string userId)
        {
            var response = await _userService.GetUserNameAsync(userId);

            if (string.IsNullOrEmpty(response))
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Create user to the system
        /// </summary>
        /// <response code="200">Create user to the system</response>
        /// <response code="400">Unable to create user</response> 
        [HttpPost(ApiRoutes.UserRoute.Create)]
        public async Task<ActionResult> Create([FromBody] CreateUserRequest request)
        {
            var created = await _userService.CreateUserAsync(request.UserName, request.Password);

            if (!created)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Delete user to the system
        /// </summary>
        /// <response code="204">Delete user from the system</response>
        /// <response code="400">Unable to create user</response> 
        /// <response code="404">Not found user</response> 
        [HttpDelete(ApiRoutes.UserRoute.Delete)]
        public async Task<ActionResult> Delete([FromRoute] string userId)
        {
            var user = await _userService.GetUserNameAsync(userId);

            if (string.IsNullOrEmpty(user))
                return NotFound();

            var deleted = await _userService.DeleteUserAsync(userId);

            if (!deleted)
                return BadRequest();

            return NoContent();
        }
    }
}