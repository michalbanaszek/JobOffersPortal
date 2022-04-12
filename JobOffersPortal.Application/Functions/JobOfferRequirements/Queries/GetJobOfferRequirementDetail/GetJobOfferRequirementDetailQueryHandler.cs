using Application.JobOfferRequirements.Queries.GetJobOfferRequirement;
using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementDetail
{
    public class GetJobOfferRequirementDetailQueryHandler : IRequestHandler<GetJobOfferRequirementDetailQuery, JobOfferRequirementDetailViewModel>
    {
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferRequirementDetailQueryHandler> _logger;

        public GetJobOfferRequirementDetailQueryHandler(IJobOfferRequirementRepository jobOfferRequirementRepository, IMapper mapper, ILogger<GetJobOfferRequirementDetailQueryHandler> logger)
        {
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<JobOfferRequirementDetailViewModel> Handle(GetJobOfferRequirementDetailQuery request, CancellationToken cancellationToken)
        {
            var entities = await _jobOfferRequirementRepository.GetByIdAsync(request.Id);

            if (entities == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferRequirement), request.Id);
            }

            return _mapper.Map<JobOfferRequirementDetailViewModel>(entities);
        }
    }
}