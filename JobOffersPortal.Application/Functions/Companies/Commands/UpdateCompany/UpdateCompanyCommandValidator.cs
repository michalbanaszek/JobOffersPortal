using FluentValidation;

namespace JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("{PropertName} Length is beewten 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");
              
        }
    }
}
