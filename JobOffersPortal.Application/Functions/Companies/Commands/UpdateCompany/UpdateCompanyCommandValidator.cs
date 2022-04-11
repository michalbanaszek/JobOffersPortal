using FluentValidation;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyCommandValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;

            RuleFor(x => x.Name)
                .MustAsync(IsNameAlreadyExist)
                .WithMessage("Company with the same Name already exist.")
                .NotEmpty()
                .WithMessage("Company cannot be empty.")
                .NotNull()
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("Company Length is between 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");                                    
        }

        private async Task<bool> IsNameAlreadyExist(string name, CancellationToken cancellationToken)
        {
            var check = await _companyRepository.IsNameAlreadyExistAsync(name);

            return !check;
        }
    }
}
