using FluentValidation;
using WebApp.ViewModels.JobOfferMvc.UpdateJobOfferMvc;

namespace WebApp.Validators.JobOfferMvcValidator
{
    public class UpdateJobOfferMvcViewModelValidator : AbstractValidator<UpdateJobOfferMvcViewModel>
    {
        public UpdateJobOfferMvcViewModelValidator()
        {

            RuleFor(x => x.Position)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("Position Length is between 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
