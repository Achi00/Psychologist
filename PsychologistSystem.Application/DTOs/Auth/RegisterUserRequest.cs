namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record RegisterUserRequest(string Firstname, string Lastname, string Email, string Password, string PasswordConfirmation);
}
