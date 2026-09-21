using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Repositories.Psychologists
{
    public interface IPsychologistApplicationRepository
    {
        void Add(PsychologistApplication application);
        Task<PsychologistApplication?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IReadOnlyList<PsychologistApplication>> GetPendingAsync(CancellationToken ct);
        // for admin review queue
        Task<PsychologistApplication?> GetPendingByUserIdAsync(Guid userId, CancellationToken ct);
    }
}
