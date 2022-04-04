using FluentValidation;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.CreateDetailsJobOfferSkillMvc;

namespace JobOffersPortal.UI.Validators.JobOfferSkillMvcValidator
{
    public class CreateDetailsJobOfferSkillMvcViewModelValidator : AbstractValidator<CreateDetailsJobOfferSkillMvcViewModel>
    {
        public CreateDetailsJobOfferSkillMvcViewModelValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}
