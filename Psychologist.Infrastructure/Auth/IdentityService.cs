using Microsoft.AspNetCore.Identity;
using PsychologistSystem.Application.Interfaces.Services.Auth;
using PsychologistSystem.Domain.Enums;
using PsychologistSystem.Persistance.Identity;

namespace PsychologistSystem.Infrastructure.Auth
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> AddToRoleAsync(Guid userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.AddToRoleAsync(user, role);

            return result.Succeeded;
        }

        public async Task<bool> CheckPasswordAsync(Guid userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return user != null && await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> ConfirmEmailAsync(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded;
        }

        public async Task<(IdentityResultStatus Status, Guid UserId, IEnumerable<string> Errors)> CreateUserAsync(string email, string firstname, string lastname, string password)
        {
            var user = new ApplicationUser
            {
                Email = email,
                FirstName = firstname, 
                LastName = lastname,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return (IdentityResultStatus.Success, user.Id, Enumerable.Empty<string>());
            }

            return (IdentityResultStatus.Failed, Guid.Empty, result.Errors.Select(e => e.Description));
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return string.Empty;
            }

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return string.Empty;
            }

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string?> GetEmailByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            
            return user?.Email;
        }

        public async Task<IList<string>> GetRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return Array.Empty<string>();
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<Guid?> GetUserIdByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user?.Id;
        }

        public async Task<bool> ResetPasswordAsync(Guid userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }
    }
}
