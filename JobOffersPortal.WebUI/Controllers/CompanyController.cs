using AutoMapper;
using JobOffersPortal.Contracts.Contracts.Queries;
using JobOffersPortal.Contracts.Contracts.Responses;
using JobOffersPortal.WebUI.Cache;
using JobOffersPortal.WebUI.Contracts.Requests;
using JobOffersPortal.WebUI.Contracts.Responses;
using JobOffersPortal.WebUI.Domain;
using JobOffersPortal.WebUI.Extensions;
using JobOffersPortal.WebUI.Helpers;
using JobOffersPortal.WebUI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IUriCompanyService _companyUriService;

        public CompanyController(ICompanyService companyService, IMapper mapper, IUriCompanyService companyUriService)
        {
            _companyService = companyService;
            _mapper = mapper;
            _companyUriService = companyUriService;
        }

        [HttpGet(ApiRoutes.Company.GetAll)]
        [Cached(50)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllByFilterQuery query, [FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = _mapper.Map<GetAllFilter>(query);

            var companies = await _companyService.GetAllIncludedAsync(filter, paginationFilter);

            var companiesResponse = _mapper.Map<List<CompanyResponse>>(companies);

            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<CompanyResponse>(companiesResponse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_companyUriService, paginationFilter, companiesResponse);

            return Ok(paginationResponse);
        }

        [HttpGet(ApiRoutes.Company.Get)]
        [Cached(50)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var company = await _companyService.GetByIdIncludedAsync(id);

            return company == null ? NotFound() : Ok(new Response<CompanyResponse>(_mapper.Map<CompanyResponse>(company)));
        }

        [HttpPut(ApiRoutes.Company.Update)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateCompanyRequest request)
        {
            var userOwnsCompany = await _companyService.UserOwnsEntityAsync(id, HttpContext.GetUserId());

            if (!userOwnsCompany)
            {
                return BadRequest(new { error = "You are not a owner this company entity." });
            }

            var company = await _companyService.GetByIdIncludedAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            _mapper.Map(request, company);

            var updated = await _companyService.UpdateAsync(company);

            return updated == false ? NotFound() : Ok(new Response<CompanyResponse>(_mapper.Map<CompanyResponse>(company)));
        }

        /// <summary>
        /// Creates a company in the system
        /// </summary>
        /// <response code="201">Creates a company in the system</response>
        /// <response code="400">Unable to create the job offer due to validation error</response>      
        [ProducesResponseType(typeof(CompanyResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [HttpPost(ApiRoutes.Company.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request)
        {
            var company = _mapper.Map<Company>(request);
            company.UserId = HttpContext.GetUserId();

            var created = await _companyService.CreateAsync(company);

            if (!created)
            {
                return BadRequest(new ErrorResponse() { Errors = new List<ErrorModel>() { new ErrorModel() { Message = "Unable to create company" } } });
            }

            var locationUri = _companyUriService.GetCompanyUri(company.Id.ToString());

            return Created(locationUri, new Response<CompanyResponse>(_mapper.Map<CompanyResponse>(company)));
        }

        [HttpDelete(ApiRoutes.Company.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var userOwnsCompany = await _companyService.UserOwnsEntityAsync(id, HttpContext.GetUserId());

            if (!userOwnsCompany)
            {
                return BadRequest(new { error = "You are not a owner this company entity." });
            }

            var company = await _companyService.DeleteAsync(id);

            return company == false ? NotFound() : NoContent();
        }
    }
}
