using AutoMapper;
using JobOffersPortal.UI.ClientServices;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.Interfaces;
using WebApp.ViewModels.JobOfferMvc.CreateJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.IndexJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.UpdateJobOfferMvc;

namespace WebApp.ClientServices
{
    public class JobOfferMvcService : IJobOfferMvcService
    {
        private readonly IMapper _mapper;
        private readonly IApiClient _client;
        private readonly IAddBearerTokenMvcService _addBearerTokenService;
        private readonly ILogger<JobOfferMvcService> _logger;

        public JobOfferMvcService(IMapper mapper, IApiClient client, IAddBearerTokenMvcService addBearerTokenService, ILogger<JobOfferMvcService> logger)
        {
            _mapper = mapper;
            _client = client;
            _addBearerTokenService = addBearerTokenService;
            _logger = logger;
        }

        public async Task<PaginatedMvcList<JobOfferMvcViewModel>> GetAllByCompany(string companyId)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var responseFromApi = await _client.JobofferGetAsync(companyId, 1, 100);

            var items = _mapper.Map<List<JobOfferMvcViewModel>>(responseFromApi.Items);

            var paginatedItems = new PaginatedMvcList<JobOfferMvcViewModel>()
            {
                PageSize = responseFromApi.PageSize,
                PageNumber = responseFromApi.PageNumber,
                TotalCount = responseFromApi.TotalCount,
                HasNextPage = responseFromApi.HasNextPage,
                HasPreviousPage = responseFromApi.HasPreviousPage,
                NextPage = responseFromApi.NextPage,
                PreviousPage = responseFromApi.PreviousPage,
                Items = items
            };

            return paginatedItems;
        }

        public async Task<JobOfferMvcViewModel> GetByIdAsync(string jobOfferId)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var responseFromApi = await _client.JobofferGetAsync(jobOfferId);

            var responseMapped = _mapper.Map<JobOfferMvcViewModel>(responseFromApi);

            return responseMapped;
        }

        public async Task<ResponseFromApi<string>> AddAsync(CreateJobOfferMvcViewModel createJobOfferViewModel)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                CreateJobOfferCommand command = _mapper.Map<CreateJobOfferCommand>(createJobOfferViewModel);

                var responseFromApi = await _client.JobofferPostAsync(command);

                if (!responseFromApi.Succeeded)
                {
                    return new ResponseFromApi<string>() { Success = false, Data = string.Empty };
                }

                return new ResponseFromApi<string>() { Data = responseFromApi.Id, Success = true };
            } 
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }

        public async Task<ResponseFromApi<string>> UpdateAsync(string jobOfferId, UpdateJobOfferMvcViewModel updateJobOfferViewModel)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                UpdateJobOfferCommand command = _mapper.Map<UpdateJobOfferCommand>(updateJobOfferViewModel);

                var responseFromApi = await _client.JobofferPutAsync(jobOfferId, command);

                if (!responseFromApi.Succeeded)
                {
                    return new ResponseFromApi<string>() { Success = false };
                }

                return new ResponseFromApi<string> { Success = true, Data = responseFromApi.Id };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }          
        }

        public async Task<ResponseFromApi<string>> DeleteAsync(string jobOfferId)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                await _client.JobofferDeleteAsync(jobOfferId);

                return new ResponseFromApi<string>() { Success = true };
            }

            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }
    }
}
