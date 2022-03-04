using FluentValidation;
using WebApp.ViewModels.JobOfferRequirementMvc.UpdateJobOfferRequirementMvc;

namespace WebApp.Validators.JobOfferRequirementMvcValidator
{
    public class UpdateJobOfferRequirementMvcViewModelValidator : AbstractValidator<UpdateJobOfferRequirementMvcViewModel>
    {
        public UpdateJobOfferRequirementMvcViewModelValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}