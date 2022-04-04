using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteJobOfferSkillCommandHandler : IRequestHandler<DeleteJobOfferSkillCommand, Unit>
    {
        private readonly ILogger<DeleteJobOfferSkillCommandHandler> _logger;
        private readonly IJobOfferSkillRepository _jobOfferSkillRepository;

        public DeleteJobOfferSkillCommandHandler(IMapper mapper, ILogger<DeleteJobOfferSkillCommandHandler> logger, IJobOfferSkillRepository jobOfferSkillRepository)
        {
            _logger = logger;
            _jobOfferSkillRepository = jobOfferSkillRepository;
        }

        public async Task<Unit> Handle(DeleteJobOfferSkillCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferSkillRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferSkill), request.Id);
            }

            await _jobOfferSkillRepository.DeleteAsync(entity);

            _logger.LogInformation("Deleted JobOfferSkill Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}