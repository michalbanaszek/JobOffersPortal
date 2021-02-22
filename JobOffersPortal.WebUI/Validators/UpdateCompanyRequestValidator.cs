using FluentValidation;
using JobOffersPortal.WebUI.Contracts.Requests;

namespace JobOffersPortal.WebUI.Validators
{
    public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
    {
        public UpdateCompanyRequestValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
