using FluentValidation;

namespace ProductService.Application.Features.Categories.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CategoryDto.Name).NotEmpty().MaximumLength(60);
    }
}