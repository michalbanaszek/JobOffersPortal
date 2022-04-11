using Application.JobOffers.Commands.CreateJobOffer;
using JobOffersPortal.API.Filters.Cache;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer;
using JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetJobOfferDetail;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    public class JobOfferController : ApiControllerBase
    {
        /// <summary>
        /// Get list of items in the system
        /// </summary>
        /// <response code="200">Get list of items in the system</response>   
        /// <response code="401">Unauthorized</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Cached(50)]
        [HttpGet(ApiRoutes.JobOfferRoute.GetAll)]
        public async Task<ActionResult<PaginatedList<JobOfferViewModel>>> GetAll([FromQuery] GetJobOffersWithPaginationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Get item in the system
        /// </summary>
        /// <response code="200">Get item in the system</response>  
        /// <response code="401">Unauthorized</response>   
        /// <response code="404">Not found item</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Cached(50)]
        [HttpGet(ApiRoutes.JobOfferRoute.Get)]
        public async Task<ActionResult<JobOfferViewModel>> Get([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new GetJobOfferDetailQuery() { Id = id }));
        }

        /// <summary>
        /// Creates a item in the system
        /// </summary>
        /// <response code="201">Creates a item in the system</response>       
        /// <response code="400">Unable to create the item due to validation error</response>   
        /// <response code="401">Unauthorized</response>   
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost(ApiRoutes.JobOfferRoute.Create)]
        public async Task<ActionResult> Create([FromBody] CreateJobOfferCommand command)
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
        /// <response code="403">User own for this entity is diffrent</response>
        /// <response code="404">Not found item</response>    
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut(ApiRoutes.JobOfferRoute.Update)]
        public async Task<ActionResult> Update([FromRoute] string id, [FromBody] UpdateJobOfferCommand command)
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(ApiRoutes.JobOfferRoute.Delete)]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await Mediator.Send(new DeleteJobOfferCommand() { Id = id });

            return NoContent();
        }
    }
}
