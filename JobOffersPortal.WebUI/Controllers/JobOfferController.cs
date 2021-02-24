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
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private readonly IJobOfferService _jobOfferService;
        private readonly IMapper _mapper;
        private readonly IUriJobOfferService _jobOfferUriService;

        public JobOfferController(IJobOfferService jobOfferService, IMapper mapper, IUriJobOfferService jobOfferUriService)
        {
            _jobOfferService = jobOfferService;
            _mapper = mapper;
            _jobOfferUriService = jobOfferUriService;
        }

        [HttpGet(ApiRoutes.JobOffer.GetAll)]
        [Cached(50)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllByFilterQuery query, [FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = _mapper.Map<GetAllFilter>(query);

            var jobsOffers = await _jobOfferService.GetAllAsync(filter, paginationFilter);

            var jobOffersResponse = _mapper.Map<List<JobOfferResponse>>(jobsOffers);

            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<JobOfferResponse>(jobOffersResponse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_jobOfferUriService, paginationFilter, jobOffersResponse);

            return Ok(paginationResponse);
        }
       
        [HttpGet(ApiRoutes.JobOffer.Get)]
        [Cached(50)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var jobOffer = await _jobOfferService.GetByIdAsync(id);

            return jobOffer == null ? NotFound() : Ok(_mapper.Map<Response<JobOfferResponse>>(jobOffer));
        }

       
        /// <summary>
        /// Creates a job offer in the system
        /// </summary>
        /// <response code="201">Creates a job offer in the system</response>
        /// <response code="400">Unable to create the job offer due to validation error</response>        
        [HttpPost(ApiRoutes.JobOffer.Create)]
        [ProducesResponseType(typeof(JobOfferResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Create([FromBody] CreateJobOfferRequest request)
        {
            var jobOffer = _mapper.Map<JobOffer>(request);
            jobOffer.UserId = HttpContext.GetUserId();

            var created = await _jobOfferService.CreateAsync(jobOffer);

            if (!created)
            {
                return BadRequest(new ErrorResponse() { Errors = new List<ErrorModel>() { new ErrorModel() { Message = "Unable to create job offer" } } });
            }

            var locationUri = _jobOfferUriService.GetJobOfferUri(jobOffer.Id.ToString());

            return Created(locationUri, _mapper.Map<Response<JobOfferResponse>>(jobOffer));
        }

        [HttpPut(ApiRoutes.JobOffer.Update)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateJobOfferRequest request)
        {
            var jobOffer = await _jobOfferService.GetByIdAsync(id);

            if (jobOffer == null)
            {
                return NotFound();
            }

            _mapper.Map(request, jobOffer);

            await _jobOfferService.UpdateAsync(jobOffer);

            return Ok(_mapper.Map<Response<JobOfferResponse>>(jobOffer));

        }

        [HttpDelete(ApiRoutes.JobOffer.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _jobOfferService.DeleteAsync(id);

            return NoContent();
        }


    }
}
