using FluentValidation;
using PsychologistSystem.Application.DTOs.Auth;

namespace PsychologistSystem.Application.Validators.Auth
{
    public sealed class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(12);
        }
    }
}
