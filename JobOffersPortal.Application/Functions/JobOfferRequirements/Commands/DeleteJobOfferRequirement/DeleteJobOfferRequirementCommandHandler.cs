using Application.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement
{
    public class DeleteJobOfferRequirementCommandHandler : IRequestHandler<DeleteJobOfferRequirementCommand, DeleteJobOfferRequirementCommandResponse>
    {
        private readonly ILogger<DeleteJobOfferRequirementCommandHandler> _logger;
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;

        public DeleteJobOfferRequirementCommandHandler(ILogger<DeleteJobOfferRequirementCommandHandler> logger, IJobOfferRequirementRepository jobOfferRequirementRepository)
        {
            _logger = logger;
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
        }

        public async Task<DeleteJobOfferRequirementCommandResponse> Handle(DeleteJobOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRequirementRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferRequirement), request.Id);
            }

            try
            {
                await _jobOfferRequirementRepository.DeleteAsync(entity);

                _logger.LogInformation("Deleted JobOfferRequirement Id: {0}", request.Id);

                return new DeleteJobOfferRequirementCommandResponse(request.Id);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new DeleteJobOfferRequirementCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new DeleteJobOfferRequirementCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}