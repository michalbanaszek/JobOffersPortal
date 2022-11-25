using Application.JobOfferRequirements.Queries.GetJobOfferRequirement;
using JobOffersPortal.API.Filters.Cache;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementDetail;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement;

namespace JobOffersPortal.API.Controllers
{
    public class JobOfferRequirementController : ApiControllerBase
    {
        /// <summary>
        /// Get list of items in the system
        /// </summary>
        /// <response code="200">Get list of items in the system</response>     
        /// <response code="401">Unauthorized</response>   
        [HttpGet(ApiRoutes.JobOfferRequirementRoute.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Cached(50)]
        public async Task<ActionResult<List<JobOfferRequirementViewModel>>> GetAll([FromRoute] string jobOfferId)
        {
            return Ok(await Mediator.Send(new GetJobOfferRequirementListQuery() { JobOfferId = jobOfferId }));
        }

        /// <summary>
        /// Get item in the system
        /// </summary>
        /// <response code="200">Get item in the system</response>   
        /// <response code="401">Unauthorized</response>   
        /// <response code="404">Not found item</response>  
        [HttpGet(ApiRoutes.JobOfferRequirementRoute.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Cached(50)]
        public async Task<ActionResult<JobOfferRequirementDetailViewModel>> Get([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new GetJobOfferRequirementDetailQuery() { Id = id }));
        }

        /// <summary>
        /// Creates a item in the system
        /// </summary>
        /// <response code="201">Creates a item in the system</response>       
        /// <response code="400">Unable to create the item due to validation error</response>       
        /// <response code="401">Unauthorized</response>   
        [HttpPost(ApiRoutes.JobOfferRequirementRoute.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Create([FromBody] CreateJobOfferRequirementCommand command)
        {
            var response = await Mediator.Send(command);

            return Created(response.Uri, null);
        }

        /// <summary>
        /// Updates a item in the system
        /// </summary>
        /// <response code="200">Updates a item in the system</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>   
        /// <response code="403">User own for this entity is different</response>
        /// <response code="404">Not found item</response>    
        [HttpPut(ApiRoutes.JobOfferRequirementRoute.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromRoute] string id, [FromBody] UpdateJobOfferRequirementCommand command)
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
        /// <response code="403">User own for this entity is different</response>
        /// <response code="404">Not found item</response>  
        [HttpDelete(ApiRoutes.JobOfferRequirementRoute.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await Mediator.Send(new DeleteJobOfferRequirementCommand() { Id = id });

            return NoContent();
        }
    }
}