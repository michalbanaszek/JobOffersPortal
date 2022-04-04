using FluentValidation;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.CreateJobOfferMvc;

namespace JobOffersPortal.UI.Validators.JobOfferMvcValidator
{
    public class CreateJobOfferMvcViewModelValidator : AbstractValidator<CreateJobOfferMvcViewModel>
    {
        public CreateJobOfferMvcViewModelValidator()
        {

            RuleFor(x => x.Position)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("Position Length is between 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}