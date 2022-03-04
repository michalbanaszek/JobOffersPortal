using FluentValidation;
using WebApp.ViewModels.JobOfferPropositionMvc.UpdateJobOfferPropositionMvc;

namespace WebApp.Validators.JobOfferPropositionMvcValidator
{
    public class UpdateJobOfferPropositionMvcViewModelValidator : AbstractValidator<UpdateJobOfferPropositionMvcViewModel>
    {
        public UpdateJobOfferPropositionMvcViewModelValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}