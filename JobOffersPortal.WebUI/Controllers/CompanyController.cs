using Application.Common.Models;
using Application.Companies.Commands.CreateCompany;
using Application.Companies.Commands.DeleteCompany;
using Application.Companies.Commands.UpdateCompany;
using Application.Companies.Queries.GetCompanies;
using Application.Companies.Queries.GetCompany;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebUI;
using WebUI.Controllers;
using WebUI.Filters.Cache;

namespace JobOffersPortal.WebUI.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CompanyController : ApiControllerBase
    {
        /// <summary>
        /// Get list of items in the system
        /// </summary>
        /// <response code="200">Get list of items in the system</response>       
        [HttpGet(ApiRoutes.CompanyRoute.GetCompanies)]       
        [ProducesResponseType(StatusCodes.Status200OK)]      
        [Cached(50)]
        public async Task<ActionResult<PaginatedList<CompanyVm>>> GetAll([FromQuery] GetCompaniesWithPaginationQuery query)
        {
            var response = await Mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Get item in the system
        /// </summary>
        /// <response code="200">Get item in the system</response>   
        /// <response code="404">Not found item</response>    
        [HttpGet(ApiRoutes.CompanyRoute.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Cached(50)]
        public async Task<ActionResult<CompanyVm>> Get([FromRoute] string id)
        {
            var response = await Mediator.Send(new GetCompanyQuery() { Id = id });          

            return Ok(response);
        }

        /// <summary>
        /// Creates a item in the system
        /// </summary>
        /// <response code="201">Creates a item in the system</response>
        /// <response code="400">Unable to create the item due to validation error</response>      
        [HttpPost(ApiRoutes.CompanyRoute.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<(Uri, string)>> Create([FromBody] CreateCompanyCommand command)
        {
            try
            {
                var response = await Mediator.Send(command);

                return Created(response.Item1, response.Item2);
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
        [HttpPut(ApiRoutes.CompanyRoute.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Update([FromRoute] string id, [FromBody] UpdateCompanyCommand command)
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
        /// <response code="201">Deletes a item in the system</response>
        /// <response code="400">User own for this entity is diffrent.</response>
        /// <response code="404">Not found item</response>  
        [HttpDelete(ApiRoutes.CompanyRoute.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await Mediator.Send(new DeleteCompanyCommand() { Id = id });

            return NoContent();
        }
    }
}
