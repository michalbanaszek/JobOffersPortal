using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillList
{
    public class GetJobOfferSkillListQueryHandler : IRequestHandler<GetJobOfferSkillListQuery, List<JobOfferSkillViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferSkillListQueryHandler> _logger;
        private readonly IJobOfferSkillRepository _jobOfferSkillRepository;

        public GetJobOfferSkillListQueryHandler(IMapper mapper, ILogger<GetJobOfferSkillListQueryHandler> logger, IJobOfferSkillRepository jobOfferSkillRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferSkillRepository = jobOfferSkillRepository;
        }

        public async Task<List<JobOfferSkillViewModel>> Handle(GetJobOfferSkillListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferSkillRepository.GetAllAsync();

            return _mapper.Map<List<JobOfferSkillViewModel>>(entity);
        }
    }
}