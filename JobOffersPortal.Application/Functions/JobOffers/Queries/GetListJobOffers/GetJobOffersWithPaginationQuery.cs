using Application.Common.Models;
using MediatR;

namespace JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers
{
    public class GetJobOffersWithPaginationQuery : IRequest<PaginatedList<JobOfferViewModel>>
    {
        public string CompanyId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
