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
                .MinimumLength(2)
                .MaximumLength(30);
        }
    }
}
