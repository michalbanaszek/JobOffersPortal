using FluentValidation;
using WebApp.ViewModels.JobOfferSkillMvc.UpdateJobOfferSkillMvc;

namespace WebApp.Validators.JobOfferSkillMvcValidator
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