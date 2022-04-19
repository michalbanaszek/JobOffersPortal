using FluentValidation;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList
{
    public class GetCompaniesListWithPaginationQueryValidator : AbstractValidator<GetCompaniesListWithPaginationQuery>
    {
        public GetCompaniesListWithPaginationQueryValidator()
        {
            RuleFor(x => x.PageSize).LessThanOrEqualTo(100);
        }
    }
}
