using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, (Uri, CreateCompanyResponse)>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;
        private readonly IUriCompanyService _uriCompanyService;

        public CreateCompanyCommandHandler(IMapper mapper, ILogger<CreateCompanyCommandHandler> logger, IApplicationDbContext context, IUriCompanyService uriCompanyService)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _uriCompanyService = uriCompanyService;
        }

        public async Task<(Uri, CreateCompanyResponse)> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
           
            var entity = _mapper.Map<Company>(request);

            _context.Companies.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Created company Id: {0}, Name: {1}", entity.Id, entity.Name);

            var uri = _uriCompanyService.GetCompanyUri(entity.Id);

            return (uri, new CreateCompanyResponse() { Id = entity.Id});
        }
    }
}
