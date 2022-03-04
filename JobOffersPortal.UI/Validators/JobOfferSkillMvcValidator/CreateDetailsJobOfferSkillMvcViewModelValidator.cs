using FluentValidation;
using WebApp.ViewModels.JobOfferSkillMvc.CreateDetailsJobOfferSkillMvc;

namespace WebApp.Validators.JobOfferSkillMvcValidator
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
