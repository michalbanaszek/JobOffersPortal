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

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionCommandHandler : IRequestHandler<CreateJobOfferPropositionCommand, CreateJobOfferPropositionCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferPropositionCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IJobOfferPropositionRepository _jobOfferPropositionRepository;
        private readonly IUriService _uriService;

        public CreateJobOfferPropositionCommandHandler(IJobOfferRepository jobOfferRepository, IMapper mapper, ILogger<CreateJobOfferPropositionCommandHandler> logger, IJobOfferPropositionRepository jobOfferPropositionRepository, IUriService uriService)
        {
            _jobOfferRepository = jobOfferRepository;
            _mapper = mapper;
            _logger = logger;
            _jobOfferPropositionRepository = jobOfferPropositionRepository;
            _uriService = uriService;
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
                Content = request.Content
            };

            entity.Propositions.Add(jobOfferProposition);

            await _jobOfferPropositionRepository.AddAsync(jobOfferProposition);

            _logger.LogInformation("Created JobOfferProposition for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

            var uri = _uriService.Get(jobOfferProposition.Id, nameof(JobOfferProposition));

            return new CreateJobOfferPropositionCommandResponse(uri);
        }
    }
}
