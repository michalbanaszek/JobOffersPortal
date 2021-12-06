using Application.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement
{
    public class DeleteOfferRequirementCommandHandler : IRequestHandler<DeleteOfferRequirementCommand, Unit>
    {
        private readonly ILogger<DeleteOfferRequirementCommandHandler> _logger;
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;

        public DeleteOfferRequirementCommandHandler(ILogger<DeleteOfferRequirementCommandHandler> logger, IJobOfferRequirementRepository jobOfferRequirementRepository)
        {
            _logger = logger;
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
        }

        public async Task<Unit> Handle(DeleteOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRequirementRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JobOfferRequirement), request.Id);
            }

            try
            {
                await _jobOfferRequirementRepository.DeleteAsync(entity);

                _logger.LogInformation("Deleted JobOfferRequirement Id: {0}", request.Id);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogWarning("DeleteOfferRequirementCommand - Exception execuded, Exception Message:", dbUpdateConcurrencyException.Message);

                throw;
            }         

            return Unit.Value;
        }
    }
}
