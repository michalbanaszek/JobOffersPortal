using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest
    {
        public string Id { get; set; }
    }

    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ILogger<DeleteCompanyCommandHandler> _logger;
        private readonly ICompanyService _companyService;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteCompanyCommandHandler(ICompanyService companyService, ILogger<DeleteCompanyCommandHandler> logger, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _companyService = companyService;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Companies.Where(x => x.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }

            var userOwns = await _companyService.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);           

            if (!userOwns)
            {
                _logger.LogWarning("Delete company failed - NotFoundUserOwnException, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new NotFoundUserOwnException(nameof(Company), _currentUserService.UserId);
            }

            _context.Companies.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted company Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
