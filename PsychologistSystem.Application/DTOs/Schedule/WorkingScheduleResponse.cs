namespace PsychologistSystem.Application.DTOs.Schedule
{
    public record WorkingScheduleResponse(Guid id, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime);
}
