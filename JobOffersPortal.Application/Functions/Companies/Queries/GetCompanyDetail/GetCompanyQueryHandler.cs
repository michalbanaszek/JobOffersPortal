using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail
{
    public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyDetailViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetCompanyQueryHandler> _logger;
        private readonly IApplicationDbContext _context;


        public GetCompanyQueryHandler(IMapper mapper, ILogger<GetCompanyQueryHandler> logger, IApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _logger = logger;
            _context = applicationDbContext;
        }

        public async Task<CompanyDetailViewModel> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Companies
                                       .Include(x => x.JobOffers)
                                            .ThenInclude(x => x.Requirements)
                                       .Include(x => x.JobOffers)
                                            .ThenInclude(x => x.Skills)
                                       .Include(x => x.JobOffers)
                                            .ThenInclude(x => x.Propositions)
                                       .SingleOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Get JobOffer failed - NotFoundException, Id: {0}", request.Id);

                throw new NotFoundException(nameof(Company), request.Id);
            }

            return _mapper.Map<CompanyDetailViewModel>(entity);
        }
    }
}
