using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer
{
    public class UpdateJobOfferCommandValidator : AbstractValidator<UpdateJobOfferCommand>
    {
        public UpdateJobOfferCommandValidator()
        {
            RuleFor(x => x.Position)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("{PropertName} Length is beewten 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");

            RuleFor(x => x.Salary)
                .NotEmpty()
                .NotNull();
        }
    }
}
