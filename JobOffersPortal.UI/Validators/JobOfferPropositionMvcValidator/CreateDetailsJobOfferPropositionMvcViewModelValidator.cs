using FluentValidation;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.CreateDetailsPropositionMvc;

namespace JobOffersPortal.UI.Validators.JobOfferPropositionMvcValidator
{
    public class CreateDetailsJobOfferPropositionMvcViewModelValidator : AbstractValidator<CreateDetailsJobOfferPropositionMvcViewModel>
    {
        public CreateDetailsJobOfferPropositionMvcViewModelValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}