using MediatR;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList
{
    public class GetCompanyListQuery : IRequest<List<CompanyListViewModel>>
    {
    }
}
