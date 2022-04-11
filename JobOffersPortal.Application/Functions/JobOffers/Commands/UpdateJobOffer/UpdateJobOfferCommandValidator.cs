using FluentValidation;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer
{
    public class UpdateJobOfferCommandValidator : AbstractValidator<UpdateJobOfferCommand>
    {
        private readonly IJobOfferRepository _jobOfferRepository;

        public UpdateJobOfferCommandValidator(IJobOfferRepository jobOfferRepository)
        {
            RuleFor(x => x.Position)
                .MustAsync(IsPositionAlreadyExist)
                .WithMessage("JobOffer with the same Position already exist.")
                .NotEmpty()
                .NotNull()
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("Position Length is between 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");

            RuleFor(x => x.Date)
               .NotEmpty()
               .NotNull()
               .LessThan(DateTime.Now.AddDays(1));

            _jobOfferRepository = jobOfferRepository;
        }

        private async Task<bool> IsPositionAlreadyExist(string position, CancellationToken cancellationToken)
        {
            var check = await _jobOfferRepository.IsPositionAlreadyExistAsync(position);

            return !check;
        }
    }
}
