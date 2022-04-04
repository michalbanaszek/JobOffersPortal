using Application.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement
{
    public class DeleteJobOfferRequirementCommandHandler : IRequestHandler<DeleteJobOfferRequirementCommand, Unit>
    {
        private readonly ILogger<DeleteJobOfferRequirementCommandHandler> _logger;
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;

        public DeleteJobOfferRequirementCommandHandler(ILogger<DeleteJobOfferRequirementCommandHandler> logger, IJobOfferRequirementRepository jobOfferRequirementRepository)
        {
            _logger = logger;
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
        }

        public async Task<Unit> Handle(DeleteJobOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRequirementRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferRequirement), request.Id);
            }

            await _jobOfferRequirementRepository.DeleteAsync(entity);

            _logger.LogInformation("Deleted JobOfferRequirement Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}