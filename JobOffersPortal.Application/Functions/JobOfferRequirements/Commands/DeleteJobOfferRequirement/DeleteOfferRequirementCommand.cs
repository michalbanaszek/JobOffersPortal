using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferRequirements.Commands.DeleteJobOfferRequirement
{
    public class DeleteOfferRequirementCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }

    public class DeleteOfferRequirementCommandHandler : IRequestHandler<DeleteOfferRequirementCommand, Unit>
    {       
        private readonly ILogger<DeleteOfferRequirementCommandHandler> _logger;
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;

        public DeleteOfferRequirementCommandHandler(IMapper mapper, ILogger<DeleteOfferRequirementCommandHandler> logger, IJobOfferRequirementRepository jobOfferRequirementRepository)
        {          
            _logger = logger;
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
        }

        public async Task<Unit> Handle(DeleteOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRequirementRepository.GetByIdAsync(request.Id);

            await _jobOfferRequirementRepository.DeleteAsync(entity);

            _logger.LogInformation("Deleted JobOfferRequirement Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
