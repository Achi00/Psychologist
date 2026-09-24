using FluentValidation;
using PsychologistSystem.Application.DTOs.Schedule;

namespace PsychologistSystem.Application.Validators.Booking
{
    public sealed class CreateWorkingScheduleRequestValidator : AbstractValidator<CreateWorkingScheduleRequest>
    {
        public CreateWorkingScheduleRequestValidator()
        {
            RuleFor(x => x.DayOfWeek)
                .IsInEnum();

            RuleFor(x => x.StartTime)
                .Must(t => t.Minute == 0 && t.Second == 0)
                .WithMessage("Start time must be on the hour.");

            RuleFor(x => x.EndTime)
                .Must(t => t.Minute == 0 && t.Second == 0)
                .WithMessage("End time must be on the hour.")
                .GreaterThan(x => x.StartTime)
                .WithMessage("End time must be after start time.");
        }
    }
}
