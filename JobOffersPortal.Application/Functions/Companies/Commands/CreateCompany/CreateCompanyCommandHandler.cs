using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyCommandResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;
        private readonly IUriService _uriService;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, ILogger<CreateCompanyCommandHandler> logger, IUriService uriService)
        {
            _companyRepository = companyRepository;
            _logger = logger;
            _uriService = uriService;
        }

        public async Task<CreateCompanyCommandResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = Company.Create(request.Name);

            await _companyRepository.AddAsync(company);

            _logger.LogInformation("Created company Id: {0}, Name: {1}", company.Id, company.Name);

            var uri = _uriService.Get(company.Id, nameof(Company));

            return new CreateCompanyCommandResponse(uri);
        }
    }
}