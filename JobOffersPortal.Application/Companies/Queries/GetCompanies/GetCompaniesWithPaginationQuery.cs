using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Companies.Queries.GetCompany;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies.Queries.GetCompanies
{
    public class GetCompaniesWithPaginationQuery : IRequest<PaginatedList<CompanyVm>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }

    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesWithPaginationQuery, PaginatedList<CompanyVm>>
    {       
        private readonly IApplicationDbContext _context;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public GetCompaniesQueryHandler(IMapper mapper, IUriService uriService, IApplicationDbContext context)
        {           
            _mapper = mapper;
            _uriService = uriService;
            _context = context;
        }

        public async Task<PaginatedList<CompanyVm>> Handle(GetCompaniesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Companies
                                 .Include(x => x.JobOffers)
                                           .ThenInclude(x => x.Requirements)
                                 .Include(x => x.JobOffers)
                                           .ThenInclude(x => x.Skills)
                                 .Include(x => x.JobOffers)
                                           .ThenInclude(x => x.Propositions)
                                 .OrderBy(x => x.Name)
                                 .ProjectTo<CompanyVm>(_mapper.ConfigurationProvider)
                                 .PaginatedListAsync(request.PageNumber, request.PageSize, _uriService);
        }
    }
}
