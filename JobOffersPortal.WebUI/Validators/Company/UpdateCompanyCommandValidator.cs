using Application.Companies.Commands.UpdateCompany;
using FluentValidation;

namespace JobOffersPortal.WebUI.Validators.Company
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
