using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillDetail
{
    public class GetJobOfferSkillDetailQueryHandler : IRequestHandler<GetJobOfferSkillDetailQuery, JobOfferSkillDetailViewModel>
    {
        private readonly IJobOfferSkillRepository _jobOfferSkillRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferSkillDetailQueryHandler> _logger;

        public GetJobOfferSkillDetailQueryHandler(IJobOfferSkillRepository jobOfferSkillRepository, IMapper mapper, ILogger<GetJobOfferSkillDetailQueryHandler> logger)
        {
            _jobOfferSkillRepository = jobOfferSkillRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<JobOfferSkillDetailViewModel> Handle(GetJobOfferSkillDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferSkillRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferSkill), request.Id);
            }

            return _mapper.Map<JobOfferSkillDetailViewModel>(entity);
        }
    }
}