using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer
{
    public class UpdateJobOfferCommandHandler : IRequestHandler<UpdateJobOfferCommand, string>
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

        public async Task<string> Handle(UpdateJobOfferCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JobOffer), request.Id);
            }

            var userOwns = await _jobOfferRepository.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("Update company failed - NotFoundUserOwnException, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new NotFoundUserOwnException(nameof(JobOffer), _currentUserService.UserId);
            }

            _mapper.Map(request, entity);

            try
            {
                await _jobOfferRepository.UpdateAsync(entity);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                    _logger.LogWarning("UpdateJobOfferCommand - Exception execuded, Exception Message:", dbUpdateConcurrencyException.Message);

                    throw;
            }

            _logger.LogInformation("Updated JobOffer Id: {0}", request.Id);

            return entity.Id;
        }
    }
}
