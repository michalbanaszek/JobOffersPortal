using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition
{
    public class UpdateJobOfferPropositionCommandHandler : IRequestHandler<UpdateJobOfferPropositionCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobOfferPropositionCommandHandler> _logger;
        private readonly IJobOfferPropositionRepository _jobOfferPropositionRepository;

        public UpdateJobOfferPropositionCommandHandler(IMapper mapper, ILogger<UpdateJobOfferPropositionCommandHandler> logger, IJobOfferPropositionRepository jobOfferPropositionRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferPropositionRepository = jobOfferPropositionRepository;
        }

        public async Task<Unit> Handle(UpdateJobOfferPropositionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                JobOfferProposition entity = new JobOfferProposition()
                {
                    Id = request.Id,
                    Content = request.Content
                };

                await _jobOfferPropositionRepository.UpdateAsync(entity);

                _logger.LogInformation("UpdateJobOfferPropositionCommand execuded.");

                return Unit.Value;
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await _jobOfferPropositionRepository.GetByIdAsync(request.Id)) == null)
                {
                    _logger.LogWarning("UpdateJobOfferPropositionCommand - NotFoundException execuded.");

                    throw new NotFoundException();
                }
                else
                {
                    _logger.LogWarning("UpdateJobOfferPropositionCommand - Exception execuded.");

                    throw;
                }
            }
        }
    }
}
