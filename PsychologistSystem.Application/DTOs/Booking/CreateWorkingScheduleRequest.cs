namespace PsychologistSystem.Application.DTOs.Booking
{
    public sealed record CreateWorkingScheduleRequest(
        DayOfWeek DayOfWeek,
        TimeOnly StartTime,
        TimeOnly EndTime
    );
}
