﻿using JobOffersPortal.API.Filters.Cache;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany;
using JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    public class CompanyController : ApiControllerBase
    {
        /// <summary>
        /// Get company list and all offers in the system
        /// </summary>
        /// <response code="200">Get list of items in the system</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet(ApiRoutes.CompanyRoute.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Cached(50)]
        public async Task<ActionResult<PaginatedList<CompanyJobOfferListViewModel>>> GetAll([FromQuery] GetCompaniesListWithPaginationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Get item in the system
        /// </summary>
        /// <response code="200">Get item in the system</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found item</response>
        [HttpGet(ApiRoutes.CompanyRoute.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Cached(50)]
        public async Task<ActionResult<CompanyJobOfferListViewModel>> Get([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new GetCompanyDetailQuery() { Id = id }));
        }

        /// <summary>
        /// Creates a item in the system
        /// </summary>
        /// <response code="201">Creates a item in the system</response>
        /// <response code="400">Unable to create the item due to validation error</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost(ApiRoutes.CompanyRoute.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Create([FromBody] CreateCompanyCommand command)
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
        [HttpPut(ApiRoutes.CompanyRoute.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromRoute] string id, [FromBody] UpdateCompanyCommand command)
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
        [HttpDelete(ApiRoutes.CompanyRoute.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await Mediator.Send(new DeleteCompanyCommand() { Id = id });

            return NoContent();
        }
    }
}