using PsychologistSystem.Application.DTOs.Auth;

namespace PsychologistSystem.Application.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<EmailConfirmationResult> RegisterAsync(RegisterUserRequest request);
        Task ConfirmEmailAsync(string userId, string token);
        Task<AuthResult> LoginAsync(LoginRequest request);
        // revokes/refreshes token
        Task LogoutAsync(Guid userId);
        Task ForgotPasswordAsync(string email);
        Task ResetPasswordAsync(ResetPasswordRequest request);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
    }
}
