using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement
{
    public class UpdateJobOfferRequirementCommandHandler : IRequestHandler<UpdateJobOfferRequirementCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobOfferRequirementCommandHandler> _logger;
        private readonly IJobOfferRequirementRepository _jobOfferRequirementRepository;

        public UpdateJobOfferRequirementCommandHandler(IMapper mapper, ILogger<UpdateJobOfferRequirementCommandHandler> logger, IJobOfferRequirementRepository jobOfferRequirementRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferRequirementRepository = jobOfferRequirementRepository;
        }

        public async Task<Unit> Handle(UpdateJobOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                JobOfferRequirement entity = new JobOfferRequirement()
                {
                    Id = request.Id,
                    Content = request.Content
                };

                await _jobOfferRequirementRepository.UpdateAsync(entity);

                _logger.LogInformation("UpdateJobOfferRequirementCommand execuded.");

                return Unit.Value;
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await _jobOfferRequirementRepository.GetByIdAsync(request.Id)) == null)
                {
                    _logger.LogWarning("UpdateJobOfferRequirementCommand - NotFoundException execuded.");

                    throw new NotFoundException();
                }
                else
                {
                    _logger.LogWarning("UpdateJobOfferRequirementCommand - Exception execuded.");

                    throw;
                }
            }
        }
    }
}
