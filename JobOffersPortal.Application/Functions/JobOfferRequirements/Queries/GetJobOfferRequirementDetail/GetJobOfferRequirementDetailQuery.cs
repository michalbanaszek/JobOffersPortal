using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementDetail;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferRequirements.Queries.GetJobOfferRequirement
{
    public class GetJobOfferRequirementDetailQuery : IRequest<JobOfferRequirementDetailViewModel>
    {
        public string Id { get; set; }
    }

    public class GetJobOfferRequirementQueryHandler : IRequestHandler<GetJobOfferRequirementDetailQuery, JobOfferRequirementDetailViewModel>
    {
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferRequirementQueryHandler> _logger;

        public GetJobOfferRequirementQueryHandler(IJobOfferRequirementRepository jobOfferRequirementRepository, IMapper mapper, ILogger<GetJobOfferRequirementQueryHandler> logger)
        {
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<JobOfferRequirementDetailViewModel> Handle(GetJobOfferRequirementDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                _logger.LogWarning("Id is null, Request ID: {0}", request.Id);

                return null;
            }

            var entities = await _jobOfferRequirementRepository.GetByIdAsync(request.Id);

            if (entities == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                return null;
            }

            return _mapper.Map<JobOfferRequirementDetailViewModel>(entities);
        }
    }
}
