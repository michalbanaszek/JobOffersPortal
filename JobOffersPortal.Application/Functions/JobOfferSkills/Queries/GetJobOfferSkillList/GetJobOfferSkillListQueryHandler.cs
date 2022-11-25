using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillList
{
    public class GetJobOfferSkillListQueryHandler : IRequestHandler<GetJobOfferSkillListQuery, JobOfferSkillViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferSkillListQueryHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;    

        public GetJobOfferSkillListQueryHandler(IMapper mapper, ILogger<GetJobOfferSkillListQueryHandler> logger, IJobOfferRepository jobOfferRepository)
        {
            _mapper = mapper;
            _logger = logger;         
            _jobOfferRepository = jobOfferRepository;
        }

        public async Task<JobOfferSkillViewModel> Handle(GetJobOfferSkillListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.JobOfferId);

            if (entity is null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.JobOfferId);

                throw new NotFoundException(nameof(JobOffer), request.JobOfferId);
            }

            return _mapper.Map<JobOfferSkillViewModel>(entity);
        }
    }
}