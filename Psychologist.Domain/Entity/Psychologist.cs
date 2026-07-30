namespace Psychologist.Domain.Entity
{
    public sealed class Psychologist
    {
        // pk + fk
        public Guid UserId { get; set; }
        public string Description { get; set; }
    }
}
