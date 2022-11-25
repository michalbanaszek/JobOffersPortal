using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement
{
    public class CreateJobOfferRequirementCommandHandler : IRequestHandler<CreateJobOfferRequirementCommand, CreateJobOfferRequirementCommandResponse>
    {
        private readonly ILogger<CreateJobOfferRequirementCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;
        private readonly IUriService _uriService;

        public CreateJobOfferRequirementCommandHandler(IJobOfferRepository jobOfferRepository, ILogger<CreateJobOfferRequirementCommandHandler> logger, IJobOfferRequirementRepository jobOfferRequirementRepository, IUriService uriService)
        {
            _jobOfferRepository = jobOfferRepository;
            _logger = logger;
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
            _uriService = uriService;
        }

        public async Task<CreateJobOfferRequirementCommandResponse> Handle(CreateJobOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            var jobOffer = await _jobOfferRepository.GetByIdIncludeAllEntities(request.JobOfferId);

            if (jobOffer is null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.JobOfferId);

                throw new NotFoundException(nameof(JobOffer), request.JobOfferId);
            }
            var requirement = jobOffer.AddRequirement(request.Content, request.JobOfferId);

            await _jobOfferRequirementRepository.AddAsync(requirement);

            _logger.LogInformation("Created CreateJobOfferRequirement for JobOffer Id: {0}, Name: {1}", jobOffer.Id, jobOffer.Position);

            var uri = _uriService.Get(requirement.Id, nameof(JobOfferRequirement));

            return new CreateJobOfferRequirementCommandResponse(uri);
        }
    }
}