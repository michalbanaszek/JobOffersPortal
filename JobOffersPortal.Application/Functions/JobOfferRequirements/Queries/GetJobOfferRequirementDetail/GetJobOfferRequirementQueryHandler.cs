using Application.JobOfferRequirements.Queries.GetJobOfferRequirement;
using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementDetail
{
    public class GetJobOfferRequirementQueryHandler : IRequestHandler<GetJobOfferRequirementDetailQuery, JobOfferRequirementDetailViewModel>
    {
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;
        private readonly IMapper _mapper;

        public GetJobOfferRequirementQueryHandler(IJobOfferRequirementRepository jobOfferRequirementRepository, IMapper mapper)
        {
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
            _mapper = mapper;
        }

        public async Task<JobOfferRequirementDetailViewModel> Handle(GetJobOfferRequirementDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entities = await _jobOfferRequirementRepository.GetByIdAsync(request.Id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<JobOfferRequirementDetailViewModel>(entities);
        }
    }
}
