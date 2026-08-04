namespace PsychologistSystem.Domain.Entity
{
    public sealed class Psychologist
    {
        // no navigation ApplicationUser, will be using joins if needed to keep reference direction correct
        public Guid UserId { get; set; }
        public string Description { get; set; }
    }
}
