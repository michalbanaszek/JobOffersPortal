using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateJobOfferSkillCommandHandler : IRequestHandler<CreateJobOfferSkillCommand, CreateJobOfferSkillResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferSkillCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;

        public CreateJobOfferSkillCommandHandler(IMapper mapper, ILogger<CreateJobOfferSkillCommandHandler> logger, IJobOfferRepository jobOfferRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferRepository = jobOfferRepository;
        }

        public async Task<CreateJobOfferSkillResponse> Handle(CreateJobOfferSkillCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.JobOfferId);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            JobOfferSkill jobOfferSkill = new JobOfferSkill()
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content
            };

            entity.Skills.Add(jobOfferSkill);

            _logger.LogInformation("Created JobOfferRequirement for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

            return _mapper.Map<CreateJobOfferSkillResponse>(entity);
        }
    }
}
