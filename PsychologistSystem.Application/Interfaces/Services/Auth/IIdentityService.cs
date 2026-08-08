using PsychologistSystem.Domain.Enums;

namespace PsychologistSystem.Application.Interfaces.Services.Auth
{
    public interface IIdentityService
    {
        Task<(IdentityResultStatus Status, Guid UserId, IEnumerable<string> Errors)> CreateUserAsync(string email, string firstname, string lastname, string password);
        Task<bool> CheckPasswordAsync(Guid userId, string password);
        Task<Guid?> GetUserIdByEmailAsync(string email);
        Task<IList<string>> GetRolesAsync(Guid userId);
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);
        Task<bool> ConfirmEmailAsync(Guid userId, string token);
        Task<string> GeneratePasswordResetTokenAsync(Guid userId);
        Task<bool> ResetPasswordAsync(Guid userId, string token, string newPassword);
        Task<bool> AddToRoleAsync(Guid userId, string role);
    }
}
