﻿using JobOffersPortal.API.Filters.Cache;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany;
using JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyListWithJobOffers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CompanyController : ApiControllerBase
    {
        /// <summary>
        /// Get company list and all offers in the system
        /// </summary>
        /// <response code="200">Get list of items in the system</response>    
        [HttpGet(ApiRoutes.CompanyRoute.GetAllCompanies)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Cached(50)]
        public async Task<ActionResult<PaginatedList<CompanyJobOfferListViewModel>>> GetAllCompaniesWithJobs([FromQuery] GetCompaniesWithJobOffersListWithPaginationQuery query)
        {
            var response = await Mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Get all company in the system
        /// </summary>
        /// <response code="200">Get list of items in the system</response>    
        [HttpGet(ApiRoutes.CompanyRoute.GetJustCompanies)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Cached(50)]
        public async Task<ActionResult<CompanyListViewModel>> GetAll()
        {
            var response = await Mediator.Send(new GetCompanyListQuery());

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
        public async Task<ActionResult<CompanyJobOfferListViewModel>> Get([FromRoute] string id)
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
        public async Task<ActionResult<CreateCompanyResponse>> Create([FromBody] CreateCompanyCommand command)
        {
            try
            {
                var response = await Mediator.Send(command);

                return Created(response.Id.ToString(), response.Id);
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
        /// <response code="204">Deletes a item in the system</response>
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
