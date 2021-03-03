using FluentValidation;
using JobOffersPortal.WebUI.Contracts.Requests;

namespace JobOffersPortal.WebUI.Validators.JobOffer
{
    public class UpdateJobOfferRequestValidator : AbstractValidator<UpdateJobOfferRequest>
    {
        public UpdateJobOfferRequestValidator()
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
