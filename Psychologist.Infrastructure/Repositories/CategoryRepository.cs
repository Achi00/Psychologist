using Microsoft.EntityFrameworkCore;
using PsychologistSystem.Application.Interfaces.Repositories.Categories;
using PsychologistSystem.Domain.Entity;
using PsychologistSystem.Persistance.Context;

namespace PsychologistSystem.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name, ct);
        }

        public async Task<List<Category>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Categories.AsNoTracking().ToListAsync(ct);
        }

        public Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _context.Categories.FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<bool> IsReferencedAsync(Guid categoryId, CancellationToken ct = default)
        {
           return await _context.Set<PsychologistCategory>().AnyAsync(pc => pc.CategoryId == categoryId, ct)
                    || await _context.PsychologistApplications.AnyAsync(a => a.CategoryId == categoryId, ct);

        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
