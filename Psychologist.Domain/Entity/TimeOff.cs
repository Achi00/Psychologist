namespace PsychologistSystem.Domain.Entity
{
    // day offs, vacations
    public sealed class TimeOff
    {
        public Guid Id { get; set; }
        public Guid PsychologistId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string? reason { get; set; }
    }
}
