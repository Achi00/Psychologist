namespace PsychologistSystem.Application.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<EmailConfirmationResult>> RegisterAsync(RegisterUserRequest request);
        Task<Result> ConfirmEmailAsync(string userId, string token);
    }
}
