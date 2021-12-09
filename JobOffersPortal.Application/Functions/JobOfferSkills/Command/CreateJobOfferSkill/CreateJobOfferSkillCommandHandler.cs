using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                throw new NotFoundException(nameof(JobOffer), request.JobOfferId);                
            }

            JobOfferSkill jobOfferSkill = new JobOfferSkill()
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content
            };

            try
            {
                entity.Skills.Add(jobOfferSkill);

                _logger.LogInformation("Created JobOfferSkill for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

                return _mapper.Map<CreateJobOfferSkillResponse>(entity);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new CreateJobOfferSkillResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new CreateJobOfferSkillResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}
