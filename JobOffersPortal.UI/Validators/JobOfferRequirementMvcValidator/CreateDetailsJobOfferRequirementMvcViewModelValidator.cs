using FluentValidation;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.CreateDetailsJobOfferRequirementMvc;

namespace JobOffersPortal.UI.Validators.JobOfferRequirementMvcValidator
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
