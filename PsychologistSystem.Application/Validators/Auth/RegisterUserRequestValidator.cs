using FluentValidation;
using PsychologistSystem.Application.DTOs.Auth;

namespace PsychologistSystem.Application.Validators.Auth
{
    public sealed class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.Firstname)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Lastname)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(12);

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.");
        }
    }
}
