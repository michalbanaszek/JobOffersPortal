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

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement
{
    public class CreateJobOfferRequirementCommandHandler : IRequestHandler<CreateJobOfferRequirementCommand, CreateJobOfferRequirementResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferRequirementCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;

        public CreateJobOfferRequirementCommandHandler(IJobOfferRepository jobOfferRepository, IMapper mapper, ILogger<CreateJobOfferRequirementCommandHandler> logger)
        {
            _jobOfferRepository = jobOfferRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateJobOfferRequirementResponse> Handle(CreateJobOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.JobOfferId);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.JobOfferId);

                throw new NotFoundException(nameof(JobOffer), request.JobOfferId);
            }

            JobOfferRequirement jobOfferRequirement = new JobOfferRequirement()
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content
            };

            try
            {
                entity.Requirements.Add(jobOfferRequirement);

                _logger.LogInformation("Created CreateJobOfferRequirementCommand for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);
              
                return _mapper.Map<CreateJobOfferRequirementResponse>(entity);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new CreateJobOfferRequirementResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new CreateJobOfferRequirementResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}