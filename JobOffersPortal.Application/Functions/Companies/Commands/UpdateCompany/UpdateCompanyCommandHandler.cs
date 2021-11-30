using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, string>
    {
        private readonly ICompanyService _companyService;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;

        public UpdateCompanyCommandHandler(ICompanyService companyService, IMapper mapper, ILogger<UpdateCompanyCommandHandler> logger, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Companies.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }

            var userOwns = await _companyService.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("Update company failed - NotFoundUserOwnException, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new NotFoundUserOwnException(nameof(Company), _currentUserService.UserId);
            }

            _mapper.Map(request, entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Updated Company Id: {0}", request.Id);

            return entity.Id;
        }
    }
}
