using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Unit>
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

        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.Id);

            if (company is null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(Company), request.Id);
            }

            var userOwns = await _companyRepository.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("User is not own for this entity, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new ForbiddenAccessException(nameof(Company), _currentUserService.UserId);
            }

            _mapper.Map(request, company);

            await _companyRepository.UpdateAsync(company);

            _logger.LogInformation("Updated Company Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}