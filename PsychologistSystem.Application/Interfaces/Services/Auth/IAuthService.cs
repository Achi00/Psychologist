using PsychologistSystem.Application.DTOs.Auth;

namespace PsychologistSystem.Application.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<EmailConfirmationResult> RegisterAsync(RegisterUserRequest request);
        Task ConfirmEmailAsync(string userId, string token);
        Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken ct = default);
        // revokes/refreshes token
        Task LogoutAsync(string rawRefreshToken);
        Task ForgotPasswordAsync(ForgotPasswordRequest request);
        Task ResetPasswordAsync(ResetPasswordRequest request);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
        // in case if user does not validate email and token expired
        Task ResendConfirmationEmailAsync(ResendConfirmationRequest request);
    }
}
