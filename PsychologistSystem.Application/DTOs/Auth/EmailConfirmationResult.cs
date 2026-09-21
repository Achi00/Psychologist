namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record EmailConfirmationResult(Guid UserId, string Token, string Email);
}
