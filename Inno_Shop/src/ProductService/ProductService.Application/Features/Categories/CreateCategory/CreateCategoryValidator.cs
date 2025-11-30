using FluentValidation;

namespace ProductService.Application.Features.Categories.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryDto).NotNull();
        
        RuleFor(c => c.CategoryDto.Name)
            .NotEmpty().WithMessage("Category name is required.") 
            .MaximumLength(60).WithMessage("Category name must not exceed 60 characters."); 
        
        RuleFor(c => c.CategoryDto.Description)
            .MaximumLength(255).WithMessage("Description is too long.");
    }
}