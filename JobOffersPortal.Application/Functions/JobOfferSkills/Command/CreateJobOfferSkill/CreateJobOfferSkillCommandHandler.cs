using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateJobOfferSkillCommandHandler : IRequestHandler<CreateJobOfferSkillCommand, CreateJobOfferSkillCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferSkillCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IJobOfferSkillRepository _jobOfferSkillRepository;
        private readonly IUriService _uriService;

        public CreateJobOfferSkillCommandHandler(IMapper mapper, ILogger<CreateJobOfferSkillCommandHandler> logger, IJobOfferRepository jobOfferRepository, IJobOfferSkillRepository jobOfferSkillRepository, IUriService uriService)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferRepository = jobOfferRepository;
            _jobOfferSkillRepository = jobOfferSkillRepository;
            _uriService = uriService;
        }

        public async Task<CreateJobOfferSkillCommandResponse> Handle(CreateJobOfferSkillCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.JobOfferId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JobOffer), request.JobOfferId);
            }

            JobOfferSkill jobOfferSkill = new JobOfferSkill()
            {    
                Content = request.Content
            };

            entity.Skills.Add(jobOfferSkill);

            await _jobOfferSkillRepository.AddAsync(jobOfferSkill);

            _logger.LogInformation("Created JobOfferSkill for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

            var uri = _uriService.Get(jobOfferSkill.Id, nameof(JobOfferSkill));

            return new CreateJobOfferSkillCommandResponse(uri);
        }
    }
}
