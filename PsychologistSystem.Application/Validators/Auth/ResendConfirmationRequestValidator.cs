using FluentValidation;
using PsychologistSystem.Application.DTOs.Auth;

namespace PsychologistSystem.Application.Validators.Auth
{
    public sealed class ResendConfirmationRequestValidator : AbstractValidator<ResendConfirmationRequest>
    {
        public ResendConfirmationRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
