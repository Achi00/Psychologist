using FluentValidation;
using PsychologistSystem.Application.DTOs.Psychologist;

namespace PsychologistSystem.Application.Validators.Psychologist
{
    public sealed class RejectApplicationRequestValidator : AbstractValidator<RejectApplicationRequest>
    {
        public RejectApplicationRequestValidator()
        {
            RuleFor(x => x.RejectionReason).NotEmpty().MaximumLength(500);
        }
    }
}
