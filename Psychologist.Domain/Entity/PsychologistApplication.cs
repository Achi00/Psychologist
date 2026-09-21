using PsychologistSystem.Domain.Enums;

namespace PsychologistSystem.Domain.Entity
{
    public sealed class PsychologistApplication
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; } = default!;
        // TODO: can be collection of different categories
        public Guid CategoryId { get; set; }
        public PsychologistApplicationStatus Status { get; set; } = PsychologistApplicationStatus.Pending;
        public DateTimeOffset SubmittedAt { get; set; }
        public DateTimeOffset? ReviewedAt { get; set; }
        // admin only
        public Guid? ReviewedByUserId { get; set; }
        public string? RejectionReason { get; set; }
    }
}
