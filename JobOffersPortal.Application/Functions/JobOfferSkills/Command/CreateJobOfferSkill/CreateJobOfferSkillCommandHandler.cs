using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateJobOfferSkillCommandHandler : IRequestHandler<CreateJobOfferSkillCommand, CreateJobOfferSkillCommandResponse>
    {
        private readonly ILogger<CreateJobOfferSkillCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IJobOfferSkillRepository _jobOfferSkillRepository;
        private readonly IUriService _uriService;

        public CreateJobOfferSkillCommandHandler(ILogger<CreateJobOfferSkillCommandHandler> logger, IJobOfferRepository jobOfferRepository, IJobOfferSkillRepository jobOfferSkillRepository, IUriService uriService)
        {
            _logger = logger;
            _jobOfferRepository = jobOfferRepository;
            _jobOfferSkillRepository = jobOfferSkillRepository;
            _uriService = uriService;
        }

        public async Task<CreateJobOfferSkillCommandResponse> Handle(CreateJobOfferSkillCommand request, CancellationToken cancellationToken)
        {
            var jobOffer = await _jobOfferRepository.GetByIdIncludeAllEntities(request.JobOfferId);

            if (jobOffer is null)
            {
                throw new NotFoundException(nameof(JobOffer), request.JobOfferId);
            }

            var skill = jobOffer.AddSkill(request.Content, request.JobOfferId);

            await _jobOfferSkillRepository.AddAsync(skill);

            _logger.LogInformation("Created JobOfferSkill for JobOffer Id: {0}, Name: {1}", jobOffer.Id, jobOffer.Position);

            var uri = _uriService.Get(skill.Id, nameof(JobOfferSkill));

            return new CreateJobOfferSkillCommandResponse(uri);
        }
    }
}
