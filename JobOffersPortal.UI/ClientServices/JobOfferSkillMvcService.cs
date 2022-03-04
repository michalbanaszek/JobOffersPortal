using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.Interfaces;
using WebApp.Services;
using WebApp.ViewModels.JobOfferSkillMvc.DetailJobOfferSkillMvc;
using WebApp.ViewModels.JobOfferSkillMvc.IndexJobOfferSkillMvc;

namespace WebApp.ClientServices
{
    public class JobOfferSkillMvcService : IJobOfferSkillMvcService
    {
        private readonly IApiClient _client;
        private readonly IMapper _mapper;
        private readonly IAddBearerTokenMvcService _addBearerTokenService;

        public JobOfferSkillMvcService(IApiClient client, IMapper mapper, IAddBearerTokenMvcService addBearerTokenService)
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
                var command = new CreateJobOfferSkillCommand() { JobOfferId = jobOfferId, Content = content };

                var responseFromApi = await _client.JobofferSkillPostAsync(command);

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
                await _client.JobofferSkillDeleteAsync(id);

                return new ResponseFromApi<string> { Success = true };
            }
            catch (ApiException ex)
            {
                return new ResponseFromApi<string>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }

        public async Task<List<JobOfferSkillMvcViewModel>> GetAllAsync(string jobOfferId)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var response = await _client.JobofferSkillsAsync(jobOfferId);

            var responseMapped = _mapper.Map<List<JobOfferSkillMvcViewModel>>(response);

            return responseMapped;
        }

        public async Task<JobOfferSkillDetailMvcViewModel> GetByIdAsync(string id)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var response = await _client.JobofferSkillGetAsync(id);

            var responseMapped = _mapper.Map<JobOfferSkillDetailMvcViewModel>(response);

            return responseMapped;
        }

        public async Task<ResponseFromApi<string>> UpdateAsync(string id, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                var command = new UpdateJobOfferSkillCommand() { Id = id, Content = content };

                var responseFromApi = await _client.JobofferSkillPutAsync(id, command);

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
