using FluentValidation;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.UpdateJobOfferPropositionMvc;

namespace JobOffersPortal.UI.Validators.JobOfferPropositionMvcValidator
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