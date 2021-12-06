using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer
{
    public class DeleteJobOfferCommandHandler : IRequestHandler<DeleteJobOfferCommand>
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

        public async Task<Unit> Handle(DeleteJobOfferCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JobOffer), request.Id);
            }

            var userOwns = await _jobOfferRepository.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("Delete company failed - NotFoundUserOwnException, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new NotFoundUserOwnException(nameof(JobOffer), _currentUserService.UserId);
            }

            try
            {
                await _jobOfferRepository.DeleteAsync(entity);

                _logger.LogInformation("Deleted JobOffer Id: {0}", entity.Id);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogWarning("DeleteJobOfferCommand - Exception execuded, Exception Message:", dbUpdateConcurrencyException.Message);

                throw;
            }

            return Unit.Value;
        }
    }
}
