using FluentValidation;

namespace JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany
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
