using AutoMapper;
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

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition
{
    public class DeleteJobOfferPropositionCommandHandler : IRequestHandler<DeleteJobOfferPropositionCommand, DeleteJobOfferPropositionCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteJobOfferPropositionCommandHandler> _logger;
        private readonly IJobOfferPropositionRepository _jobOfferPropositionRepository;

        public DeleteJobOfferPropositionCommandHandler(IMapper mapper, ILogger<DeleteJobOfferPropositionCommandHandler> logger, IJobOfferPropositionRepository jobOfferPropositionRepository, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferPropositionRepository = jobOfferPropositionRepository;
        }

        public async Task<DeleteJobOfferPropositionCommandResponse> Handle(DeleteJobOfferPropositionCommand request, CancellationToken cancellationToken)
        {            
            var entity = await _jobOfferPropositionRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferProposition), request.Id);
            }

            try
            {
                await _jobOfferPropositionRepository.DeleteAsync(entity);

                _logger.LogInformation("Deleted JobOfferProposition Id: {0}", request.Id);

                return new DeleteJobOfferPropositionCommandResponse(request.Id);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new DeleteJobOfferPropositionCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new DeleteJobOfferPropositionCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}
