using FluentValidation;
using PsychologistSystem.Application.DTOs.Category;
using PsychologistSystem.Application.Exceptions;
using PsychologistSystem.Application.Interfaces;
using PsychologistSystem.Application.Interfaces.Repositories.Categories;
using PsychologistSystem.Application.Interfaces.Services.Categories;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Services.Categories
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateCategoryRequest> _createValidator;
        private readonly IValidator<UpdateCategoryRequest> _updateValidator;

        public CategoryService(
            ICategoryRepository categoryRepository, 
            IUnitOfWork unitOfWork,
            IValidator<CreateCategoryRequest> createValidator,
            IValidator<UpdateCategoryRequest> updateValidator
        )
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<Guid> CreateAsync(CreateCategoryRequest request, CancellationToken ct = default)
        {
            await _createValidator.ValidateAndThrowAsync(request, ct);

            if (await _categoryRepository.ExistsByNameAsync(request.Name, ct))
            {
                throw new InvalidOperationException("A category with this name already exists");
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
            };

            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(ct);

            return category.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var category = await _categoryRepository.GetByIdAsync(id, ct);

            if (category is null)
            {
                throw new NotFoundException("Category not found");
            }

            _categoryRepository.Remove(category);

            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<List<CategoryResponse>> GetAllAsync(CancellationToken ct = default)
        {
            var categories = await _categoryRepository.GetAllAsync(ct);

            return categories.Select(c => new CategoryResponse(c.Id, c.Name, c.Description)).ToList();
        }

        public async Task<CategoryResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var category = await _categoryRepository.GetByIdAsync(id, ct);

            if (category is null)
            {
                throw new NotFoundException("Category not found");
            }

            return new CategoryResponse(category.Id, category.Name, category.Description);
        }

        public async Task UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct = default)
        {
            await _updateValidator.ValidateAndThrowAsync(request, ct);

            var category = await _categoryRepository.GetByIdAsync(id, ct);

            if (category is null)
            {
                throw new NotFoundException("Category not found.");
            }

            category.Name = request.Name;
            category.Description = request.Description;

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
