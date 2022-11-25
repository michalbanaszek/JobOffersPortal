using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
    {
        private readonly ILogger<DeleteCompanyCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICompanyRepository _companyRepository;


        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository, ILogger<DeleteCompanyCommandHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _companyRepository.GetByIdAsync(request.Id);

            if (entity is null)
            {
                _logger.LogError("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(Company), request.Id);
            }

            var userOwns = await _companyRepository.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogError("User is not own for this entity, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new ForbiddenAccessException(nameof(Company), request.Id);
            }

            await _companyRepository.DeleteAsync(entity);

            _logger.LogInformation("Deleted company Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
