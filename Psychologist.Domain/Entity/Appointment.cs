using Psychologist.Domain.Enums;

namespace Psychologist.Domain.Entity
{
    //TODO: considering AvailabilitySlot in future
    public sealed class Appointment
    {
        public Guid Id { get; set; }
        public Guid PsychologistId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
