using FluentValidation;

namespace ProductService.Application.Features.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
public CreateProductCommandValidator()
{
    // Проверяем, что вложенный DTO не является null
    RuleFor(c => c.Product).NotNull();
        
    // Устанавливаем правила для полей внутри DTO, используя каскадный режим для удобства
    When(c => c.Product != null, () =>
    {
        RuleFor(c => c.Product.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(c => c.Product.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(c => c.Product.CategoryId)
            .NotEmpty().WithMessage("Category is required.");
    });
}
}