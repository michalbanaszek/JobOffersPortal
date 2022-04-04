using AutoMapper;
using JobOffersPortal.UI.Interfaces;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.DetailJobOfferSkillMvc;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.IndexJobOfferSkillMvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.ClientServices
{
    public class JobOfferSkillMvcService : IJobOfferSkillMvcService
    {
        private readonly IApiClient _client;
        private readonly IMapper _mapper;
        private readonly IAddBearerTokenMvcService _addBearerTokenService;
        private readonly ILogger<JobOfferSkillMvcService> _logger;

        public JobOfferSkillMvcService(IApiClient client, IMapper mapper, IAddBearerTokenMvcService addBearerTokenService, ILogger<JobOfferSkillMvcService> logger)
        {
            _client = client;
            _mapper = mapper;
            _addBearerTokenService = addBearerTokenService;
            _logger = logger;
        }

        public async Task AddAsync(string jobOfferId, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var command = new CreateJobOfferSkillCommand() { JobOfferId = jobOfferId, Content = content };

            await _client.JobofferSkillPostAsync(command);
        }

        public async Task DeleteAsync(string id)
        {
            _addBearerTokenService.AddBearerToken(_client);

            await _client.JobofferSkillDeleteAsync(id);
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

        public async Task UpdateAsync(string id, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var command = new UpdateJobOfferSkillCommand() { Id = id, Content = content };

            await _client.JobofferSkillPutAsync(id, command);
        }
    }
}