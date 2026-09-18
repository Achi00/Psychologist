namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record ConfirmEmailRequest(string UserId, string Token);

}
