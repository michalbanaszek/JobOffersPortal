using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteOfferSkillCommandHandler : IRequestHandler<DeleteOfferSkillCommand, Unit>
    {       
        private readonly ILogger<DeleteOfferSkillCommandHandler> _logger;
        private readonly IJobOfferSkillRepository _jobOfferSkillRepository;

        public DeleteOfferSkillCommandHandler(IMapper mapper, ILogger<DeleteOfferSkillCommandHandler> logger, IJobOfferSkillRepository jobOfferSkillRepository, ICurrentUserService currentUserService)
        {            
            _logger = logger;
            _jobOfferSkillRepository = jobOfferSkillRepository;
        }

        public async Task<Unit> Handle(DeleteOfferSkillCommand request, CancellationToken cancellationToken)
        {
            var jobOfferSkill = await _jobOfferSkillRepository.GetByIdAsync(request.Id);

            await _jobOfferSkillRepository.DeleteAsync(jobOfferSkill);

            _logger.LogInformation("Deleted JobOfferSkill Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
