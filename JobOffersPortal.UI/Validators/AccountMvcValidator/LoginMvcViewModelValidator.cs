using FluentValidation;
using JobOffersPortal.UI.ViewModels.AccountMvc.LoginAccountMvc;

namespace JobOffersPortal.UI.Validators.AccountMvcValidator
{
    public class LoginMvcViewModelValidator : AbstractValidator<LoginMvcViewModel>
    {
        public LoginMvcViewModelValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}