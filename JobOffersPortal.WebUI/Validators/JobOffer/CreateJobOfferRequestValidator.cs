using FluentValidation;
using JobOffersPortal.WebUI.Contracts.Requests;

namespace JobOffersPortal.WebUI.Validators.JobOffer
{
    public class CreateJobOfferRequestValidator : AbstractValidator<CreateJobOfferRequest>
    {
        public CreateJobOfferRequestValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Position)
                .NotEmpty()
                .NotNull();
        }
    }
}
