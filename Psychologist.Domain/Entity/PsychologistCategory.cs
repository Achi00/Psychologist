namespace PsychologistSystem.Domain.Entity
{
    public sealed class PsychologistCategory
    {
        public Guid PsychologistId { get; set; }
        public Guid CategoryId { get; set; }
        public Psychologist Psychologist { get; set; } = default!;
        public Category Category { get; set; } = default!;
    }
}
