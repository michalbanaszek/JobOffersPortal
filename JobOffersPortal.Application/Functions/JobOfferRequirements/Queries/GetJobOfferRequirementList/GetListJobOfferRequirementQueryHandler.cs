using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementList
{
    public class GetListJobOfferRequirementQueryHandler : IRequestHandler<GetJobOfferRequirementListQuery, List<JobOfferRequirementViewModel>>
    {
        private readonly IMapper _mapper;       
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;

        public GetListJobOfferRequirementQueryHandler(IMapper mapper, IJobOfferRequirementRepository jobOfferRequirementRepository)
        {
            _mapper = mapper;          
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
        }

        public async Task<List<JobOfferRequirementViewModel>> Handle(GetJobOfferRequirementListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRequirementRepository.GetAllAsync();

            return _mapper.Map<List<JobOfferRequirementViewModel>>(entity);
        }
    }
}
