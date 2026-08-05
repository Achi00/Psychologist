namespace PsychologistSystem.Domain.Entity
{
    // defines calendar data and aveilable work times for booking
    public sealed class WorkingSchedule
    {
        public Guid Id { get; set; }
        public Guid PsychologistId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
