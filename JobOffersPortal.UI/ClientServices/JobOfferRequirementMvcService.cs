using AutoMapper;
using JobOffersPortal.UI.Interfaces;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.DetailJobOfferRequirementMvc;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.IndexJobOfferRequirementMvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.ClientServices
{
    public class JobOfferRequirementMvcService : IJobOfferRequirementMvcService
    {
        private readonly IApiClient _client;
        private readonly IMapper _mapper;
        private readonly IAddBearerTokenMvcService _addBearerTokenService;
        private readonly ILogger<JobOfferRequirementMvcService> _logger;

        public JobOfferRequirementMvcService(IApiClient client, IMapper mapper, IAddBearerTokenMvcService addBearerTokenService, ILogger<JobOfferRequirementMvcService> logger)
        {
            _client = client;
            _mapper = mapper;
            _addBearerTokenService = addBearerTokenService;
            _logger = logger;
        }

        public async Task AddAsync(string jobOfferId, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var command = new CreateJobOfferRequirementCommand() { JobOfferId = jobOfferId, Content = content };

            await _client.JobofferRequirementPostAsync(command);
        }

        public async Task DeleteAsync(string id)
        {
            _addBearerTokenService.AddBearerToken(_client);

            await _client.JobofferRequirementDeleteAsync(id);
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

        public async Task UpdateAsync(string id, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var command = new UpdateJobOfferRequirementCommand() { Id = id, Content = content };

            await _client.JobofferRequirementPutAsync(id, command);
        }
    }
}
