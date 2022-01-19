using Application.JobOfferPropositions.Commands.CreateJobOfferProposition;
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

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionCommandHandler : IRequestHandler<CreateJobOfferPropositionCommand, CreateJobOfferPropositionCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferPropositionCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IJobOfferPropositionRepository _jobOfferPropositionRepository;

        public CreateJobOfferPropositionCommandHandler(IJobOfferRepository jobOfferRepository, IMapper mapper, ILogger<CreateJobOfferPropositionCommandHandler> logger, IJobOfferPropositionRepository jobOfferPropositionRepository)
        {
            _jobOfferRepository = jobOfferRepository;
            _mapper = mapper;
            _logger = logger;
            _jobOfferPropositionRepository = jobOfferPropositionRepository;
        }

        public async Task<CreateJobOfferPropositionCommandResponse> Handle(CreateJobOfferPropositionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.JobOfferId);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.JobOfferId);

                throw new NotFoundException(nameof(JobOffer), request.JobOfferId);
            }

            JobOfferProposition jobOfferProposition = new JobOfferProposition()
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content
            };

            try
            {
                entity.Propositions.Add(jobOfferProposition);

                await _jobOfferPropositionRepository.AddAsync(jobOfferProposition);

                _logger.LogInformation("Created JobOfferProposition for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

                return new CreateJobOfferPropositionCommandResponse(true, new string[] { }) { Id = entity.Id };
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new CreateJobOfferPropositionCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new CreateJobOfferPropositionCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}
