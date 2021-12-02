using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
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

            await _companyRepository.AddAsync(entity);

            //_context.Companies.Add(entity);

            //await _context.SaveChangesAsync(cancellationToken);

            //_logger.LogInformation("Created company Id: {0}, Name: {1}", entity.Id, entity.Name);

            // var uri = _uriCompanyService.GetCompanyUri(entity.Id);

            return new CreateCompanyResponse(entity.Id);
        }
    }
}
