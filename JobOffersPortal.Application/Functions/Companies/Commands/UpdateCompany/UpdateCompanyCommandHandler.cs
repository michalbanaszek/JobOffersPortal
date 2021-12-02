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
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, string>
    {
        private readonly ICompanyRepository _companyRepository;  
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(ICompanyRepository companyService, IMapper mapper, ILogger<UpdateCompanyCommandHandler> logger, ICurrentUserService currentUserService)
        {
            _companyRepository = companyService;          
            _logger = logger;         
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<string> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _companyRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }

            var userOwns = await _companyRepository.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("Update company failed - NotFoundUserOwnException, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new NotFoundUserOwnException(nameof(Company), _currentUserService.UserId);
            }

            _mapper.Map(request, entity);

            await _companyRepository.UpdateAsync(entity);

            _logger.LogInformation("Updated Company Id: {0}", request.Id);

            return entity.Id;
        }
    }
}
