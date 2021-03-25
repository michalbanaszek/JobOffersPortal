using Application.Companies.Commands.CreateCompany;
using FluentValidation;

namespace JobOffersPortal.WebUI.Validators.Company
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
