using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
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
                throw new NotFoundException();
            }

            JobOfferRequirement jobOfferRequirement = new JobOfferRequirement()
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content
            };

            entity.Requirements.Add(jobOfferRequirement);

            _logger.LogInformation("Created JobOfferRequirement for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

            return _mapper.Map<CreateJobOfferRequirementResponse>(entity);
        }
    }
}
