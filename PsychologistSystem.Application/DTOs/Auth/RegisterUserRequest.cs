namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record RegisterUserRequest(string UserName, string Email, string Password);
}
