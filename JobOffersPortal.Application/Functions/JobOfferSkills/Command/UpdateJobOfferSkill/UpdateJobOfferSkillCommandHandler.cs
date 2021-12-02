using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                JobOfferSkill entity = new JobOfferSkill()
                {
                    Id = request.Id,
                    Content = request.Content
                };

                await _jobOfferSkillRepository.UpdateAsync(entity);

                _logger.LogInformation("UpdateJobOfferSkillCommand execuded.");

                return Unit.Value;
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await _jobOfferSkillRepository.GetByIdAsync(request.Id)) == null)
                {
                    _logger.LogWarning("UpdateJobOfferPropositionCommand - NotFoundException execuded.");

                    throw new NotFoundException();
                }
                else
                {
                    _logger.LogWarning("UpdateJobOfferPropositionCommand - Exception execuded.");

                    throw;
                }
            }
        }
    }
}
