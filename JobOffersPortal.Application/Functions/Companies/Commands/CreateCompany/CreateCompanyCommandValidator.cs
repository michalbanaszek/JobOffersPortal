using FluentValidation;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
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
