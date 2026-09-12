using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Repositories.Psychologists
{
    public interface IPsychologistApplicationRepository
    {
        void Add(PsychologistApplication application);
        Task<PsychologistApplication?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<PsychologistApplication>> GetPendingAsync();
        Task<PsychologistApplication?> GetPendingByUserIdAsync(Guid userId);
    }
}
