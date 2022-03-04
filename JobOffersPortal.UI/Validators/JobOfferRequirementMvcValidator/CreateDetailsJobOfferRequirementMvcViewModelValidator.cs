using FluentValidation;
using WebApp.ViewModels.JobOfferRequirementMvc.CreateDetailsJobOfferRequirrementMvc;

namespace WebApp.Validators.JobOfferRequirementMvcValidator
{
    public class CreateDetailsJobOfferRequirementMvcViewModelValidator : AbstractValidator<CreateDetailsJobOfferRequirementMvcViewModel>
    {
        public CreateDetailsJobOfferRequirementMvcViewModelValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}
