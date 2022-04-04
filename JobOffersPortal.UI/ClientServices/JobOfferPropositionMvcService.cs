using AutoMapper;
using JobOffersPortal.UI.Interfaces;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.DetailJobOfferPropositionMvc;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.IndexJobOfferPropositionMvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.ClientServices
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

        public async Task AddAsync(string jobOfferId, string content)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var command = new CreateJobOfferPropositionCommand() { JobOfferId = jobOfferId, Content = content };

            await _client.JobofferPropositionPostAsync(command);
        }

        public async Task DeleteAsync(string id)
        {
            _addBearerTokenService.AddBearerToken(_client);

            await _client.JobofferPropositionDeleteAsync(id);
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

        public async Task UpdateAsync(string id, string content)
        {
            var command = new UpdateJobOfferPropositionCommand() { Id = id, Content = content };

            _addBearerTokenService.AddBearerToken(_client);

            await _client.JobofferPropositionPutAsync(id, command);
        }
    }
}
