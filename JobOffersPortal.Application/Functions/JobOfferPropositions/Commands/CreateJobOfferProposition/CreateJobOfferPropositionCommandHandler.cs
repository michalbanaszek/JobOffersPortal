﻿using Application.JobOfferPropositions.Commands.CreateJobOfferProposition;
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
    public class CreateJobOfferPropositionCommandHandler : IRequestHandler<CreateJobOfferPropositionCommand, CreateJobOfferPropositionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferPropositionCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;

        public CreateJobOfferPropositionCommandHandler(IJobOfferRepository jobOfferRepository, IMapper mapper, ILogger<CreateJobOfferPropositionCommandHandler> logger)
        {
            _jobOfferRepository = jobOfferRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateJobOfferPropositionResponse> Handle(CreateJobOfferPropositionCommand request, CancellationToken cancellationToken)
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

                _logger.LogInformation("Created JobOfferProposition for JobOffer Id: {0}, Name: {1}, JobOfferId: {2}", entity.Id, entity.Position);

                return _mapper.Map<CreateJobOfferPropositionResponse>(entity);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new CreateJobOfferPropositionResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new CreateJobOfferPropositionResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}
