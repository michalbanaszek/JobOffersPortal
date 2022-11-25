using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionDetail
{
    public class GetJobOfferPropositionDetailQueryHandler : IRequestHandler<GetJobOfferPropositionDetailQuery, JobOfferPropositionDetailViewModel>
    {
        private readonly ILogger<GetJobOfferPropositionDetailQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IJobOfferPropositionRepository _jobOfferPropositionRepository;

        public GetJobOfferPropositionDetailQueryHandler(IJobOfferPropositionRepository jobOfferPropositionRepository, IMapper mapper, ILogger<GetJobOfferPropositionDetailQueryHandler> logger)
        {
            _jobOfferPropositionRepository = jobOfferPropositionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<JobOfferPropositionDetailViewModel> Handle(GetJobOfferPropositionDetailQuery request, CancellationToken cancellationToken)
        {
            var entities = await _jobOfferPropositionRepository.GetByIdAsync(request.Id);

            if (entities is null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferProposition), request.Id);
            }

            return _mapper.Map<JobOfferPropositionDetailViewModel>(entities);         
        }
    }
}
