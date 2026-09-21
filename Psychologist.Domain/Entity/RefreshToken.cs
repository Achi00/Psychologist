namespace PsychologistSystem.Domain.Entity
{
    public sealed class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string TokenHash { get; set; } = default!;
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? RevokedAt { get; set; }
        // for rotation tracking
        public string? ReplacedByTokenHash { get; set; }

        public bool IsActive => RevokedAt is null && DateTimeOffset.UtcNow < ExpiresAt;
    }
}
