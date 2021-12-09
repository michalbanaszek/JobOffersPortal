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

namespace JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, UpdateCompanyCommandResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(ICompanyRepository companyService, IMapper mapper, ILogger<UpdateCompanyCommandHandler> logger, ICurrentUserService currentUserService)
        {
            _companyRepository = companyService;
            _logger = logger;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<UpdateCompanyCommandResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _companyRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(Company), request.Id);
            }

            var userOwns = await _companyRepository.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("User is not own for this entity, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new ForbiddenAccessException(nameof(Company), request.Id);
            }

            try
            {
                _mapper.Map(request, entity);

                await _companyRepository.UpdateAsync(entity);

                _logger.LogInformation("Updated Company Id: {0}", request.Id);

                return new UpdateCompanyCommandResponse(request.Id);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new UpdateCompanyCommandResponse(false, new string[] { "Cannot update the entity to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new UpdateCompanyCommandResponse(false, new string[] { "Cannot update the entity to database." });
            }
        }
    }
}