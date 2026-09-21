using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Repositories.Psychologists
{
    public interface IPsychologistRepository
    {
        void Add(Psychologist psychologist);
        Task<Psychologist?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<Psychologist?> GetByUserIdAsync(Guid userId, CancellationToken ct);
    }
}
