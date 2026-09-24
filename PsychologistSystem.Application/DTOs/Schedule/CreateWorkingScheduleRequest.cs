namespace PsychologistSystem.Application.DTOs.Schedule
{
    public sealed record CreateWorkingScheduleRequest(
        DayOfWeek DayOfWeek,
        TimeOnly StartTime,
        TimeOnly EndTime
    );
}
