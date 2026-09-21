namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record AuthResult(string AccessToken, DateTime ExpiresAt, string? RefreshToken = null);

}
