using FluentValidation;
using JobOffersPortal.WebUI.Contracts.Requests;

namespace JobOffersPortal.WebUI.Validators
{
    public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
