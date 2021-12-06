using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;
        private readonly IUriCompanyService _uriCompanyService;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper, ILogger<CreateCompanyCommandHandler> logger, IUriCompanyService uriCompanyService)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
            _uriCompanyService = uriCompanyService;
        }

        public async Task<CreateCompanyResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Company>(request);

            try
            {
                await _companyRepository.AddAsync(entity);

                _logger.LogInformation("Created company Id: {0}, Name: {1}", entity.Id, entity.Name);             
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogWarning("CreateCompanyCommand - Exception execuded, Exception Message:", dbUpdateConcurrencyException.Message);

                throw;
            }

            var uri = _uriCompanyService.GetCompanyUri(entity.Id);

            return new CreateCompanyResponse(entity.Id, uri);
        }
    }
}
