namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record ResetPasswordRequest(Guid UserId, string Token, string NewPassword);
}
