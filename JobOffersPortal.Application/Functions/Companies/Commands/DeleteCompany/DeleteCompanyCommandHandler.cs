using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
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

            if (entity == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }

            var userOwns = await _companyRepository.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("Delete company failed - NotFoundUserOwnException, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new NotFoundUserOwnException(nameof(Company), _currentUserService.UserId);
            }

            try
            {
                await _companyRepository.DeleteAsync(entity);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogWarning("DeleteCompanyCommand - Exception execuded, Exception Message:", dbUpdateConcurrencyException.Message);

                throw;
            }

            _logger.LogInformation("Deleted company Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
