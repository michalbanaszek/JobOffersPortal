using FluentValidation;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer
{
    public class CreateJobOfferCommandValidator : AbstractValidator<CreateJobOfferCommand>
    {
        private readonly IJobOfferRepository _jobOfferRepository;

        public CreateJobOfferCommandValidator(IJobOfferRepository jobOfferRepository)
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Position)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("Position Length is between 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");

            RuleFor(x => x.Date)
                .NotEmpty()
                .NotNull()
                .LessThan(DateTime.Now.AddDays(1));

            RuleFor(x => x.Requirements)
                .NotNull();

            RuleFor(x => x.Skills)
                .NotNull();

            RuleFor(x => x.Propositions)
                .NotNull();

            _jobOfferRepository = jobOfferRepository;
        }
    }
}
