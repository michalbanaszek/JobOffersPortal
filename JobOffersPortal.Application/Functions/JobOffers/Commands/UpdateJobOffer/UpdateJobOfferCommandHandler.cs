using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer
{
    public class UpdateJobOfferCommandHandler : IRequestHandler<UpdateJobOfferCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobOfferCommandHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;

        public UpdateJobOfferCommandHandler(IJobOfferRepository jobOfferRepository, IMapper mapper, ILogger<UpdateJobOfferCommandHandler> logger, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _logger = logger;
            _jobOfferRepository = jobOfferRepository;
        }

        public async Task<Unit> Handle(UpdateJobOfferCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.Id);

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

            _mapper.Map(request, entity);

            await _jobOfferRepository.UpdateAsync(entity);

            _logger.LogInformation("Updated JobOffer Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}