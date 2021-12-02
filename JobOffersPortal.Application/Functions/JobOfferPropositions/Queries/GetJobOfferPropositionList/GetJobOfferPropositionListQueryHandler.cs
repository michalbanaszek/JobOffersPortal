using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionList
{
    public class GetJobOfferPropositionListQueryHandler : IRequestHandler<GetJobOfferPropositionListQuery, List<JobOfferPropositionViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferPropositionListQueryHandler> _logger;
        private readonly IJobOfferPropositionRepository _jobOfferPropositionRepository;

        public GetJobOfferPropositionListQueryHandler(IMapper mapper, ILogger<GetJobOfferPropositionListQueryHandler> logger, IJobOfferPropositionRepository jobOfferPropositionRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferPropositionRepository = jobOfferPropositionRepository;
        }

        public async Task<List<JobOfferPropositionViewModel>> Handle(GetJobOfferPropositionListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferPropositionRepository.GetAllAsync();

            return _mapper.Map<List<JobOfferPropositionViewModel>>(entity);
        }
    }
}
