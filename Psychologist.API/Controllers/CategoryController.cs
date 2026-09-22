using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologistSystem.Application.DTOs.Category;
using PsychologistSystem.Application.Interfaces.Services.Categories;
using PsychologistSystem.Domain.Enums;

namespace PsychologistSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            return Ok(await _categoryService.GetAllAsync(ct));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            return Ok(await _categoryService.GetByIdAsync(id, ct));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Create(CreateCategoryRequest request, CancellationToken ct)
        {
            var id = await _categoryService.CreateAsync(request, ct);
            return StatusCode(StatusCodes.Status201Created, new { id });
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request, CancellationToken ct)
        {
            await _categoryService.UpdateAsync(id, request, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _categoryService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
