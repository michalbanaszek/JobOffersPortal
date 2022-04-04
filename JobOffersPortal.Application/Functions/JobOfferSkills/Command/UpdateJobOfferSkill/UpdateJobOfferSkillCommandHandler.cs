using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.UpdateJobOfferSkill
{
    public class UpdateJobOfferSkillCommandHandler : IRequestHandler<UpdateJobOfferSkillCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobOfferSkillCommandHandler> _logger;
        private readonly IJobOfferSkillRepository _jobOfferSkillRepository;

        public UpdateJobOfferSkillCommandHandler(IMapper mapper, ILogger<UpdateJobOfferSkillCommandHandler> logger, IJobOfferSkillRepository jobOfferSkillRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferSkillRepository = jobOfferSkillRepository;
        }

        public async Task<Unit> Handle(UpdateJobOfferSkillCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferSkillRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferSkill), request.Id);
            }

            _mapper.Map(request, entity);

            await _jobOfferSkillRepository.UpdateAsync(entity);

            _logger.LogInformation("Updated JobOfferSkill Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}