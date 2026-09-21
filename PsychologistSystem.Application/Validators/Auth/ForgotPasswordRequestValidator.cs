using FluentValidation;
using PsychologistSystem.Application.DTOs.Auth;

namespace PsychologistSystem.Application.Validators.Auth
{
    public sealed class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
