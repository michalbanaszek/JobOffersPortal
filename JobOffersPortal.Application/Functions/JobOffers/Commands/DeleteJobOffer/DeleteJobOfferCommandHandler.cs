using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer
{
    public class DeleteJobOfferCommandHandler : IRequestHandler<DeleteJobOfferCommand, DeleteJobOfferCommandResponse>
    {
        private readonly ILogger<DeleteJobOfferCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteJobOfferCommandHandler(IJobOfferRepository jobOfferRepository, ILogger<DeleteJobOfferCommandHandler> logger, ICurrentUserService currentUserService)
        {
            _jobOfferRepository = jobOfferRepository;
            _logger = logger;        
            _currentUserService = currentUserService;
        }

        public async Task<DeleteJobOfferCommandResponse> Handle(DeleteJobOfferCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOffer), request.Id);
            }

            var userOwns = await _jobOfferRepository.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("User is not own for this entity, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new ForbiddenAccessException(nameof(JobOffer), request.Id);
            }

            try
            {
                await _jobOfferRepository.DeleteAsync(entity);

                _logger.LogInformation("Deleted JobOffer Id: {0}", entity.Id);

                return new DeleteJobOfferCommandResponse(entity.Id);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new DeleteJobOfferCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new DeleteJobOfferCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}