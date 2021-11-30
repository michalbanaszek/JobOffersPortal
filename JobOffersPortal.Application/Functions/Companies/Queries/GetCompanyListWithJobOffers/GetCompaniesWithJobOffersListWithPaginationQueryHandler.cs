using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList
{
    public class GetCompaniesWithJobOffersListWithPaginationQueryHandler : IRequestHandler<GetCompaniesWithJobOffersListWithPaginationQuery, PaginatedList<CompanyJobOfferListViewModel>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public GetCompaniesWithJobOffersListWithPaginationQueryHandler(IMapper mapper, IUriService uriService, IApplicationDbContext context)
        {
            _mapper = mapper;
            _uriService = uriService;
            _context = context;
        }

        public async Task<PaginatedList<CompanyJobOfferListViewModel>> Handle(GetCompaniesWithJobOffersListWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Companies
                                 .Include(x => x.JobOffers)
                                           .ThenInclude(x => x.Requirements)
                                 .Include(x => x.JobOffers)
                                           .ThenInclude(x => x.Skills)
                                 .Include(x => x.JobOffers)
                                           .ThenInclude(x => x.Propositions)
                                 .OrderBy(x => x.Name)
                                 .ProjectTo<CompanyJobOfferListViewModel>(_mapper.ConfigurationProvider)
                                 .PaginatedListAsync(request.PageNumber, request.PageSize, _uriService);
        }
    }
}
