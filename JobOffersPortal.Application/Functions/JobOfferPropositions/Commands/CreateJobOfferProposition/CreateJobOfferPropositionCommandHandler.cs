using Application.JobOfferPropositions.Commands.CreateJobOfferProposition;
using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
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
        private IJobOfferRepository _jobOfferRepository;

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
                throw new NotFoundException();
            }

            JobOfferProposition jobOfferProposition = new JobOfferProposition()
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content
            };

            entity.Propositions.Add(jobOfferProposition);
            
            _logger.LogInformation("Created JobOfferProposition for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

            return _mapper.Map<CreateJobOfferPropositionResponse>(entity);      
        }
    }
}
