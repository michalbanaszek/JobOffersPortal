using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition
{
    public class DeleteJobOfferPropositionCommandHandler : IRequestHandler<DeleteJobOfferPropositionCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteJobOfferPropositionCommandHandler> _logger;
        private readonly IJobOfferPropositionRepository _jobOfferPropositionRepository;

        public DeleteJobOfferPropositionCommandHandler(IMapper mapper, ILogger<DeleteJobOfferPropositionCommandHandler> logger, IJobOfferPropositionRepository jobOfferPropositionRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferPropositionRepository = jobOfferPropositionRepository;
        }

        public async Task<Unit> Handle(DeleteJobOfferPropositionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferPropositionRepository.GetByIdAsync(request.Id);

            if (entity is null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferProposition), request.Id);
            }

            await _jobOfferPropositionRepository.DeleteAsync(entity);

            _logger.LogInformation("Deleted JobOfferProposition Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
