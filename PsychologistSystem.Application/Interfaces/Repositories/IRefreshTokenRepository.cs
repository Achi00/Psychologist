using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);
        Task RevokeAsync(Guid tokenId);
        // log out everywhere
        Task RevokeAllForUserAsync(Guid userId);
    }
}
