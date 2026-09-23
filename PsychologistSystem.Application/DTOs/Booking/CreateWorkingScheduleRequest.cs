namespace PsychologistSystem.Application.DTOs.Booking
{
    public record CreateWorkingScheduleRequest(DateTimeOffset StartTime, DateTimeOffset EndTime);
}
