using JobOffersPortal.API.Filters.Cache;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill;
using JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill;
using JobOffersPortal.Application.Functions.JobOfferSkills.Command.UpdateJobOfferSkill;
using JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillDetail;
using JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    public class JobOfferSkillController : ApiControllerBase
    {
        /// <summary>
        /// Get list of items in the system
        /// </summary>
        /// <response code="200">Get list of items in the system</response>       
        /// <response code="401">Unauthorized</response>   
        [HttpGet(ApiRoutes.JobOfferSkillRoute.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Cached(50)]
        public async Task<ActionResult<List<JobOfferSkillViewModel>>> GetAll([FromRoute] string jobOfferId)
        {
            return Ok(await Mediator.Send(new GetJobOfferSkillListQuery() { JobOfferId = jobOfferId }));
        }

        /// <summary>
        /// Get item in the system
        /// </summary>
        /// <response code="200">Get item in the system</response>   
        /// <response code="401">Unauthorized</response>   
        /// <response code="404">Not found item</response>  
        [HttpGet(ApiRoutes.JobOfferSkillRoute.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Cached(50)]
        public async Task<ActionResult<JobOfferSkillDetailViewModel>> Get([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new GetJobOfferSkillDetailQuery() { Id = id }));
        }

        /// <summary>
        /// Creates a item in the system
        /// </summary>
        /// <response code="201">Creates a item in the system</response>       
        /// <response code="400">Unable to create the item due to validation error</response>    
        /// <response code="401">Unauthorized</response>   
        [HttpPost(ApiRoutes.JobOfferSkillRoute.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Create([FromBody] CreateJobOfferSkillCommand command)
        {
            var response = await Mediator.Send(command);

            return Created(response.Uri.ToString(), null);
        }

        /// <summary>
        /// Updates a item in the system
        /// </summary>
        /// <response code="200">Updates a item in the system</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>   
        /// <response code="403">User own for this entity is diffrent</response>
        /// <response code="404">Not found item</response>    
        [HttpPut(ApiRoutes.JobOfferSkillRoute.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromRoute] string id, [FromBody] UpdateJobOfferSkillCommand command)
        {
            if (id != command.Id)            
                return BadRequest();            

            await Mediator.Send(command);
           
            return Ok();
        }

        /// <summary>
        /// Deletes a item in the system
        /// </summary>
        /// <response code="204">Deletes a item in the system</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>   
        /// <response code="403">User own for this entity is diffrent</response>
        /// <response code="404">Not found item</response>  
        [HttpDelete(ApiRoutes.JobOfferSkillRoute.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await Mediator.Send(new DeleteJobOfferSkillCommand() { Id = id });

            return NoContent();
        }
    }
}