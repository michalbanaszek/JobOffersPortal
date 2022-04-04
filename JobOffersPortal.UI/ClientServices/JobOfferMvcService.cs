using AutoMapper;
using JobOffersPortal.UI.Interfaces;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.CreateJobOfferMvc;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.IndexJobOfferMvc;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.UpdateJobOfferMvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.ClientServices
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

        public async Task AddAsync(CreateJobOfferMvcViewModel createJobOfferViewModel)
        {
            _addBearerTokenService.AddBearerToken(_client);

            CreateJobOfferCommand command = _mapper.Map<CreateJobOfferCommand>(createJobOfferViewModel);

            await _client.JobofferPostAsync(command);
        }

        public async Task UpdateAsync(string jobOfferId, UpdateJobOfferMvcViewModel updateJobOfferViewModel)
        {
            _addBearerTokenService.AddBearerToken(_client);

            UpdateJobOfferCommand command = _mapper.Map<UpdateJobOfferCommand>(updateJobOfferViewModel);

            await _client.JobofferPutAsync(jobOfferId, command);
        }

        public async Task DeleteAsync(string jobOfferId)
        {
            _addBearerTokenService.AddBearerToken(_client);

            await _client.JobofferDeleteAsync(jobOfferId);
        }
    }
}
