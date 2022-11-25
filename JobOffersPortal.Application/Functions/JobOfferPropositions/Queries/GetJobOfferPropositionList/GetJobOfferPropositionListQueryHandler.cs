using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionList
{
    public class GetJobOfferPropositionListQueryHandler : IRequestHandler<GetJobOfferPropositionListQuery, JobOfferPropositionViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferPropositionListQueryHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;

        public GetJobOfferPropositionListQueryHandler(IMapper mapper, ILogger<GetJobOfferPropositionListQueryHandler> logger, IJobOfferRepository jobOfferRepository = null)
        {
            _mapper = mapper;
            _jobOfferRepository = jobOfferRepository;
            _logger = logger;
        }

        public async Task<JobOfferPropositionViewModel> Handle(GetJobOfferPropositionListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.JobOfferId);

            if (entity is null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.JobOfferId);

                throw new NotFoundException(nameof(JobOffer), request.JobOfferId);
            }

            return _mapper.Map<JobOfferPropositionViewModel>(entity);
        }
    }
}
