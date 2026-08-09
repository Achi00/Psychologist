using Microsoft.AspNetCore.Identity;
using PsychologistSystem.Persistance.Identity;

namespace PsychologistSystem.Infrastructure.Validators
{
    public sealed class PreventCurrentPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        private readonly IPasswordHasher<ApplicationUser> _hasher;

        public PreventCurrentPasswordValidator(IPasswordHasher<ApplicationUser> hasher)
        {
            _hasher = hasher;
        }
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
        {
            if (password is null)
            {
                return Task.FromResult(IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "PasswordNull",
                        Description = "Password can not be null."
                    }
                ));
            }

            // password hash comparison
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash!, password);

            return Task.FromResult(result == PasswordVerificationResult.Success
            ? IdentityResult.Failed(new IdentityError { Code = "PasswordReused", Description = "New password can't match current password." })
            : IdentityResult.Success);
        }
    }
}
