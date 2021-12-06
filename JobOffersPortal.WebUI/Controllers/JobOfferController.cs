using Application.JobOffers.Commands.CreateJobOffer;
using JobOffersPortal.API.Filters.Cache;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer;
using JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetJobOfferDetail;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]   
    public class JobOfferController : ApiControllerBase
    {
        /// <summary>
        /// Get list of items in the system
        /// </summary>
        /// <response code="200">Get list of items in the system</response>       
        [HttpGet(ApiRoutes.JobOfferRoute.GetJobOffers)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Cached(50)]
        public async Task<ActionResult<PaginatedList<JobOfferViewModel>>> GetAll([FromQuery] GetJobOffersWithPaginationQuery query)
        {
            var response = await Mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Get item in the system
        /// </summary>
        /// <response code="200">Get item in the system</response>   
        /// <response code="404">Not found item</response>  
        [HttpGet(ApiRoutes.JobOfferRoute.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Cached(50)]
        public async Task<ActionResult<JobOfferViewModel>> Get([FromRoute] string id)
        {
            var response = await Mediator.Send(new GetJobOfferDetailQuery() { Id = id });

            return Ok(response);
        }

        /// <summary>
        /// Creates a item in the system
        /// </summary>
        /// <response code="201">Creates a item in the system</response>       
        /// <response code="400">Unable to create the item due to validation error</response>        
        [HttpPost(ApiRoutes.JobOfferRoute.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateJobOfferCommand command)
        {
            try
            {
                var response = await Mediator.Send(command);

                return Created(response.Url, response.Id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates a item in the system
        /// </summary>
        /// <response code="200">Updates a item in the system</response>
        /// <response code="400">User own for this entity is diffrent.</response>
        /// <response code="404">Not found item</response>    
        [HttpPut(ApiRoutes.JobOfferRoute.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Update([FromRoute] string id, [FromBody] UpdateJobOfferCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a item in the system
        /// </summary>
        /// <response code="204">Deletes a item in the system</response>
        /// <response code="400">User own for this entity is diffrent.</response>
        /// <response code="404">Not found item</response>  
        [HttpDelete(ApiRoutes.JobOfferRoute.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await Mediator.Send(new DeleteJobOfferCommand() { Id = id });

            return NoContent();
        }
    }
}
