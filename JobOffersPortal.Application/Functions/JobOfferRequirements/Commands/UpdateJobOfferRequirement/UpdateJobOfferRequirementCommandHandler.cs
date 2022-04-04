using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement
{
    public class UpdateJobOfferRequirementCommandHandler : IRequestHandler<UpdateJobOfferRequirementCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobOfferRequirementCommandHandler> _logger;
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;

        public UpdateJobOfferRequirementCommandHandler(IMapper mapper, ILogger<UpdateJobOfferRequirementCommandHandler> logger, IJobOfferRequirementRepository jobOfferRequirementRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
        }

        public async Task<Unit> Handle(UpdateJobOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRequirementRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferRequirement), request.Id);
            }

            _mapper.Map(request, entity);

            await _jobOfferRequirementRepository.UpdateAsync(entity);

            _logger.LogInformation("Updated JobOfferRequirement Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}