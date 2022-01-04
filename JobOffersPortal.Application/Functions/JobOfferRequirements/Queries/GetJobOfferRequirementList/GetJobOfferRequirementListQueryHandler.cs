using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementList
{
    public class GetJobOfferRequirementListQueryHandler : IRequestHandler<GetJobOfferRequirementListQuery, JobOfferRequirementViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferRequirementListQueryHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;

        public GetJobOfferRequirementListQueryHandler(IMapper mapper, IJobOfferRepository jobOfferRepository, ILogger<GetJobOfferRequirementListQueryHandler> logger)
        {
            _mapper = mapper;
            _jobOfferRepository = jobOfferRepository;
            _logger = logger;          
        }

        public async Task<JobOfferRequirementViewModel> Handle(GetJobOfferRequirementListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.JobOfferId);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.JobOfferId);

                throw new NotFoundException(nameof(JobOffer), request.JobOfferId);
            }

            return _mapper.Map<JobOfferRequirementViewModel>(entity);
        }
    }
}