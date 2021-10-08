using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.JobOffers.Queries.GetJobOffer;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOffers.Queries.GetListJobOffers
{
    public class GetJobOffersWithPaginationQuery : IRequest<PaginatedList<JobOfferViewModel>>
    {
        public string CompanyId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }

    public class GetJobOffersWithPaginationQueryHandler : IRequestHandler<GetJobOffersWithPaginationQuery, PaginatedList<JobOfferViewModel>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public GetJobOffersWithPaginationQueryHandler(IMapper mapper, IApplicationDbContext context, IUriService uriService)
        {
            _mapper = mapper;
            _context = context;
            _uriService = uriService;
        }

        public async Task<PaginatedList<JobOfferViewModel>> Handle(GetJobOffersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.JobOffers
                                 .Include(x => x.Requirements)
                                 .Include(x => x.Skills)
                                 .Include(x => x.Propositions)
                                 .Where(x => x.CompanyId == request.CompanyId)
                                 .OrderBy(x => !x.IsAvailable)
                                 .ProjectTo<JobOfferViewModel>(_mapper.ConfigurationProvider)
                                 .PaginatedListAsync(request.PageNumber, request.PageSize, _uriService); 
        } 
           
        
    }
}
