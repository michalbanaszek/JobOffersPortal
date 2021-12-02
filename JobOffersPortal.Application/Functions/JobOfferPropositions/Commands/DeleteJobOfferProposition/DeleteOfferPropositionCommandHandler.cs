using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using MediatR;
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

            await _jobOfferPropositionRepository.DeleteAsync(jobOfferProposition);

            _logger.LogInformation("Deleted JobOfferProposition Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
