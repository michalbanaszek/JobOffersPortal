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

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteJobOfferSkillCommandHandler : IRequestHandler<DeleteJobOfferSkillCommand, DeleteJobOfferSkillCommandResponse>
    {       
        private readonly ILogger<DeleteJobOfferSkillCommandHandler> _logger;
        private readonly IJobOfferSkillRepository _jobOfferSkillRepository;

        public DeleteJobOfferSkillCommandHandler(IMapper mapper, ILogger<DeleteJobOfferSkillCommandHandler> logger, IJobOfferSkillRepository jobOfferSkillRepository, ICurrentUserService currentUserService)
        {            
            _logger = logger;
            _jobOfferSkillRepository = jobOfferSkillRepository;
        }

        public async Task<DeleteJobOfferSkillCommandResponse> Handle(DeleteJobOfferSkillCommand request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferSkillRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOfferSkill), request.Id);
            }

            try
            {
                await _jobOfferSkillRepository.DeleteAsync(entity);

                _logger.LogInformation("Deleted JobOfferSkill Id: {0}", request.Id);

                return new DeleteJobOfferSkillCommandResponse(request.Id);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new DeleteJobOfferSkillCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new DeleteJobOfferSkillCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}