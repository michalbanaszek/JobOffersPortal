using FluentValidation;
using System.Linq;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList
{
    public class GetCompaniesListWithPaginationQueryValidator : AbstractValidator<GetCompaniesListWithPaginationQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };

        public GetCompaniesListWithPaginationQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
