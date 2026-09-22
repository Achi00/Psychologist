using PsychologistSystem.Application.DTOs.Category;

namespace PsychologistSystem.Application.Interfaces.Services.Categories
{
    public interface ICategoryService
    {
        Task<Guid> CreateAsync(CreateCategoryRequest request, CancellationToken ct = default);
        Task UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<CategoryResponse> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<CategoryResponse>> GetAllAsync(CancellationToken ct = default);
    }
}
