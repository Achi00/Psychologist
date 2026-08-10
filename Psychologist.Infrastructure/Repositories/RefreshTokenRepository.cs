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
        public Task AddAsync(RefreshToken token)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken?> GetByTokenHashAsync(string tokenHash)
        {
            throw new NotImplementedException();
        }

        public Task RevokeAllForUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task RevokeAsync(Guid tokenId)
        {
            throw new NotImplementedException();
        }
    }
}
