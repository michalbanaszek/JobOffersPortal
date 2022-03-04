using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.Interfaces;
using WebApp.Services;
using WebApp.ViewModels.JobOfferRequirementMvc.DetailJobOfferRequirementMvc;
using WebApp.ViewModels.JobOfferRequirementMvc.IndexJobOfferRequirementMvc;

namespace WebApp.ClientServices
{
    public class JobOfferRequirementMvcService : IJobOfferRequirementMvcService
    {
        private readonly IApiClient _client;
        private readonly IMapper _mapper;
        private readonly IAddBearerTokenMvcService _addBearerTokenService;

        public JobOfferRequirementMvcService(IApiClient client, IMapper mapper, IAddBearerTokenMvcService addBearerTokenService)
        {
            _client = client;
            _mapper = mapper;
            _addBearerTokenService = addBearerTokenService;
        }

        public async Task<ResponseFromApi<string>> AddAsync(string jobOfferId, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                var command = new CreateJobOfferRequirementCommand() { JobOfferId = jobOfferId, Content = content };

                var responseFromApi = await _client.JobofferRequirementPostAsync(command);

                if (!responseFromApi.Succeeded)
                {
                    return new ResponseFromApi<string>() { Success = false, Errors = responseFromApi.Errors };
                }

                return new ResponseFromApi<string> { Success = true, Data = responseFromApi.Id };
            }
            catch (ApiException ex)
            {
                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }

        public async Task<ResponseFromApi<string>> DeleteAsync(string id)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                await _client.JobofferRequirementDeleteAsync(id);

                return new ResponseFromApi<string> { Success = true };
            }
            catch (ApiException ex)
            {
                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }

        public async Task<List<JobOfferRequirementMvcViewModel>> GetAllAsync(string jobOfferId)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var response = await _client.JobofferRequirementsAsync(jobOfferId);

            var responseMapped = _mapper.Map<List<JobOfferRequirementMvcViewModel>>(response);

            return responseMapped;
        }

        public async Task<JobOfferRequirementDetailMvcViewModel> GetByIdAsync(string id)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var response = await _client.JobofferRequirementGetAsync(id);

            var responseMapped = _mapper.Map<JobOfferRequirementDetailMvcViewModel>(response);

            return responseMapped;
        }

        public async Task<ResponseFromApi<string>> UpdateAsync(string id, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                var command = new UpdateJobOfferRequirementCommand() { Id = id, Content = content };

                var responseFromApi = await _client.JobofferRequirementPutAsync(id, command);

                if (!responseFromApi.Succeeded)
                {
                    return new ResponseFromApi<string>() { Success = false, Errors = responseFromApi.Errors };
                }

                return new ResponseFromApi<string> { Success = true, Data = responseFromApi.Id };
            }
            catch (ApiException ex)
            {
                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }
    }
}
