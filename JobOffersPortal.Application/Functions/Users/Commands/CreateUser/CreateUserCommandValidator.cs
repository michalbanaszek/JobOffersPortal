using FluentValidation;
using JobOffersPortal.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserService _userService;
        public CreateUserCommandValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.Email)
                 .MustAsync(IsNameAlreadyExist)
                 .WithMessage("User with the same Email already exist.")
                 .NotEmpty()
                 .WithMessage("User cannot be empty.")
                 .NotNull()
                 .MinimumLength(2).MaximumLength(30)
                 .WithMessage("User Length is between 2 and 30")
                 .EmailAddress();                 
        }

        private async Task<bool> IsNameAlreadyExist(string name, CancellationToken cancellationToken)
        {
            var check = await _userService.IsUserEmailAlreadyExistAsync(name);

            return !check;
        }
    }
}
