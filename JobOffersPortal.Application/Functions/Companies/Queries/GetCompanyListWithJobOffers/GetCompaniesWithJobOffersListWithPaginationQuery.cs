using Application.Common.Models;
using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList
{
    public class GetCompaniesWithJobOffersListWithPaginationQuery : IRequest<PaginatedList<CompanyJobOfferListViewModel>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
