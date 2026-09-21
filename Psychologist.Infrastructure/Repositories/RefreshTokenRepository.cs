using Microsoft.EntityFrameworkCore;
using PsychologistSystem.Application.Interfaces.Repositories;
using PsychologistSystem.Domain.Entity;
using PsychologistSystem.Persistance.Context;

namespace PsychologistSystem.Infrastructure.Repositories
{
    public sealed class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
        }

        public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);
        }

        public async Task RevokeAllForUserAsync(Guid userId, CancellationToken ct = default)
        {
            var refreshToken = await _context.RefreshTokens.Where(rt => rt.UserId == userId && rt.RevokedAt == null).ToListAsync(ct);

            foreach (var token in refreshToken)
            {
                token.RevokedAt = DateTimeOffset.UtcNow;
            }
        }

        public async Task RevokeAsync(Guid tokenId)
        {
            var token = await _context.RefreshTokens.FindAsync(tokenId);

            if (token is not null)
            {
                token.RevokedAt = DateTimeOffset.UtcNow;
            }
        }
    }
}
