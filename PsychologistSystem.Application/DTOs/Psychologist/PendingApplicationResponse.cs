namespace PsychologistSystem.Application.DTOs.Psychologist
{
    public sealed record PendingApplicationResponse(Guid Id, Guid UserId, string Description, Guid CategoryId, DateTimeOffset SubmittedAt);
}
