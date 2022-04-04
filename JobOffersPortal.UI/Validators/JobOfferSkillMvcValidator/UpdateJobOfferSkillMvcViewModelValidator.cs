using FluentValidation;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.UpdateJobOfferSkillMvc;

namespace JobOffersPortal.UI.Validators.JobOfferSkillMvcValidator
{
    public class UpdateJobOfferSkillMvcViewModelValidator : AbstractValidator<UpdateJobOfferSkillMvcViewModel>
    {
        public UpdateJobOfferSkillMvcViewModelValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}