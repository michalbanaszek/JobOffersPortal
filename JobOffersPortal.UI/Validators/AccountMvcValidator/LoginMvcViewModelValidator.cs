using FluentValidation;
using WebApp.ViewModels.AccountMvc.LoginAccountMvc;

namespace WebApp.Validators.AccountMvcValidator
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