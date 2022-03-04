using FluentValidation;
using WebApp.ViewModels.JobOfferMvc.CreateJobOfferMvc;

namespace WebApp.Validators.JobOfferMvcValidator
{
    public class CreateJobOfferMvcViewModelValidator : AbstractValidator<CreateJobOfferMvcViewModel>
    {
        public CreateJobOfferMvcViewModelValidator()
        {
            RuleFor(x => x.Position)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(30);
        }
    }
}