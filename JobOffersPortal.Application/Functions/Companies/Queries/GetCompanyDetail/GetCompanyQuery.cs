using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail
{
    public class GetCompanyQuery : IRequest<CompanyDetailViewModel>
    {
        public string Id { get; set; }
    }
}
