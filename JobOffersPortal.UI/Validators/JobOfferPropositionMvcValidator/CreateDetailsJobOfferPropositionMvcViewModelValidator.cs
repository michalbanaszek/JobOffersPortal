using FluentValidation;
using WebApp.ViewModels.JobOfferPropositionMvc.CreateDetailsPropositionMvc;

namespace WebApp.Validators.JobOfferPropositionMvcValidator
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