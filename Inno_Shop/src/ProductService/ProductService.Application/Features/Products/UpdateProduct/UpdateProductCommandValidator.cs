using FluentValidation;

namespace ProductService.Application.Features.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        // Валидируем ID, который приходит из URL
        RuleFor(c => c.Id).NotEmpty();

        // Валидируем DTO, который приходит из тела запроса
        RuleFor(c => c.Product).NotNull();
        RuleFor(c => c.Product.Name).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Product.Price).GreaterThan(0);
        RuleFor(c => c.Product.CategoryId).NotEmpty();
    }
}