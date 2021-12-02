using FluentValidation;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyCommandValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;

            RuleFor(x => x.Name)
                .MustAsync(IsNameAlreadyExist)
                .WithMessage("Company with the same Name already exist.")
                .NotEmpty()
                .WithMessage("Company cannot be empty.")
                .NotNull()
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("{PropertName} Length is beewten 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");
        }

        private async Task<bool> IsNameAlreadyExist(string name, CancellationToken cancellationToken)
        {
            var check = await _companyRepository.IsNameAlreadyExistAsync(name);

            return !check;
        }
    }
}
