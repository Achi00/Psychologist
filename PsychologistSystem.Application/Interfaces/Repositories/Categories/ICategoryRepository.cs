using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Repositories.Categories
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        void Remove(Category category);
        Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Category>> GetAllAsync(CancellationToken ct = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
        Task<bool> IsReferencedAsync(Guid id, CancellationToken ct);
    }
}
