using PsychologistSystem.Application.DTOs.Auth;

namespace PsychologistSystem.Application.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<EmailConfirmationResult> RegisterAsync(RegisterUserRequest request);
        Task ConfirmEmailAsync(string userId, string token);
    }
}
