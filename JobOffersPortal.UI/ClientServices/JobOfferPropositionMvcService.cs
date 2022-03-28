using AutoMapper;
using JobOffersPortal.UI.ClientServices;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.Interfaces;
using WebApp.ViewModels.JobOfferPropositionMvc.DetailJobOfferPropositionMvc;
using WebApp.ViewModels.JobOfferPropositionMvc.IndexJobOfferPropositionMvc;

namespace WebApp.ClientServices
{
    public class JobOfferPropositionMvcService : IJobOfferPropositionMvcService
    {
        private readonly IApiClient _client;
        private readonly IMapper _mapper;
        private readonly IAddBearerTokenMvcService _addBearerTokenService;
        private readonly ILogger<JobOfferPropositionMvcService> _logger;

        public JobOfferPropositionMvcService(IApiClient client, IMapper mapper, IAddBearerTokenMvcService addBearerTokenService, ILogger<JobOfferPropositionMvcService> logger)
        {
            _client = client;
            _mapper = mapper;
            _addBearerTokenService = addBearerTokenService;
            _logger = logger;
        }

        public async Task<ResponseFromApi<string>> AddAsync(string jobOfferId, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                var command = new CreateJobOfferPropositionCommand() { JobOfferId = jobOfferId, Content = content };

                var responseFromApi = await _client.JobofferPropositionPostAsync(command);

                if (!responseFromApi.Succeeded)
                {
                    return new ResponseFromApi<string>() { Success = false, Errors = responseFromApi.Errors };
                }

                return new ResponseFromApi<string> { Success = true, Data = responseFromApi.Id };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }

        public async Task<ResponseFromApi<string>> DeleteAsync(string id)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                await _client.JobofferPropositionDeleteAsync(id);

                return new ResponseFromApi<string> { Success = true };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }

        public async Task<List<JobOfferPropositionMvcViewModel>> GetAllAsync(string jobOfferId)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var response = await _client.JobofferPropositionsAsync(jobOfferId);

            var responseMapped = _mapper.Map<List<JobOfferPropositionMvcViewModel>>(response);

            return responseMapped;
        }

        public async Task<JobOfferPropositionDetailMvcViewModel> GetByIdAsync(string id)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var response = await _client.JobofferPropositionGetAsync(id);

            var responseMapped = _mapper.Map<JobOfferPropositionDetailMvcViewModel>(response);

            return responseMapped;
        }

        public async Task<ResponseFromApi<string>> UpdateAsync(string id, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                var command = new UpdateJobOfferPropositionCommand() { Id = id, Content = content };

                var responseFromApi = await _client.JobofferPropositionPutAsync(id, command);

                if (!responseFromApi.Succeeded)
                {
                    return new ResponseFromApi<string>() { Success = false, Errors = responseFromApi.Errors };
                }

                return new ResponseFromApi<string> { Success = true, Data = responseFromApi.Id };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }
    }
}
