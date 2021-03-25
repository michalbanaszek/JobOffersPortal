using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<(Uri, string)>, IMapFrom<Company>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCompanyCommand, Company>();
        }
    }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, (Uri, string)>
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

        public async Task<(Uri, string)> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Company>(request);

            _context.Companies.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Created company Id: {0}, Name: {1}", entity.Id, entity.Name);

            var uri = _uriCompanyService.GetCompanyUri(entity.Id);

            return (uri, entity.Id);
        }
    }
}
