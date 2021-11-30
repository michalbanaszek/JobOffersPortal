using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer
{
    public class UpdateJobOfferCommandValidator : AbstractValidator<UpdateJobOfferCommand>
    {
        public UpdateJobOfferCommandValidator()
        {
            RuleFor(x => x.Position)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Salary)
                .NotEmpty()
                .NotNull();
        }
    }
}
