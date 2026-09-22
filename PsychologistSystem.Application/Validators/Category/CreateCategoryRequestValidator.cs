using FluentValidation;
using PsychologistSystem.Application.DTOs.Category;

namespace PsychologistSystem.Application.Validators.Category
{
    public sealed class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        }
    }
}