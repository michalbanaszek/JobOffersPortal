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

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition
{
    public class DeleteOfferPropositionCommandHandler : IRequestHandler<DeleteOfferPropositionCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOfferPropositionCommandHandler> _logger;
        private readonly IJobOfferPropositionRepository _jobOfferPropositionRepository;

        public DeleteOfferPropositionCommandHandler(IMapper mapper, ILogger<DeleteOfferPropositionCommandHandler> logger, IJobOfferPropositionRepository jobOfferPropositionRepository, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferPropositionRepository = jobOfferPropositionRepository;
        }

        public async Task<Unit> Handle(DeleteOfferPropositionCommand request, CancellationToken cancellationToken)
        {            
            var jobOfferProposition = await _jobOfferPropositionRepository.GetByIdAsync(request.Id);

            if (jobOfferProposition == null)
            {
                throw new NotFoundException(nameof(JobOfferProposition), request.Id);
            }

            try
            {
                await _jobOfferPropositionRepository.DeleteAsync(jobOfferProposition);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogWarning("DeleteOfferPropositionCommand - Exception execuded, Exception Message:", dbUpdateConcurrencyException.Message);

                throw;
            }

            _logger.LogInformation("Deleted JobOfferProposition Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
