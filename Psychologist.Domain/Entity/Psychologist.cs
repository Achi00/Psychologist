namespace PsychologistSystem.Domain.Entity
{
    public sealed class Psychologist
    {
        // no navigation ApplicationUser, will be using joins if needed to keep reference direction correct
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }

        // navigation
        public ICollection<WorkingSchedule> WorkingSchedules { get; set; } = new List<WorkingSchedule>();

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
