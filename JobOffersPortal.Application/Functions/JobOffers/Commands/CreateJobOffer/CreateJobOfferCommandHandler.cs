﻿using Application.JobOffers.Commands.CreateJobOffer;
using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer
{
    public class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, CreateJobOfferCommandResponse>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferCommandHandler> _logger;
        private readonly IUriJobOfferService _uriJobOfferService;

        public CreateJobOfferCommandHandler(IMapper mapper, ILogger<CreateJobOfferCommandHandler> logger, IJobOfferRepository jobOfferRepository, IUriJobOfferService uriJobOfferService)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferRepository = jobOfferRepository;
            _uriJobOfferService = uriJobOfferService;
        }

        public async Task<CreateJobOfferCommandResponse> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<JobOffer>(request);

                await _jobOfferRepository.AddAsync(entity);

                _logger.LogInformation("Created JobOffer Id: {0}", entity.Id);

                var uri = _uriJobOfferService.GetJobOfferUri(entity.Id);

                return new CreateJobOfferCommandResponse(true, new string[] {}) { Id = entity.Id, Uri = uri };
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new CreateJobOfferCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new CreateJobOfferCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}