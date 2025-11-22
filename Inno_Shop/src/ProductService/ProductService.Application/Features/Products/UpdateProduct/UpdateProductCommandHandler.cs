using AutoMapper;
using MediatR;
using ProductService.Contracts;
using ProductService.Entities.Exceptions;

namespace ProductService.Application.Features.Products.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService; // <-- ВНЕДРЯЕМ НАШ СЕРВИС

    public UpdateProductCommandHandler(IRepositoryManager repository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUserService = currentUserService; // <-- СОХРАНЯЕМ ЕГО
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = await _repository.Product.GetProductByIdAsync(request.Id, trackChanges: true);
        if (productEntity is null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        // --- ВОТ ОНА, ПРОВЕРКА ПРАВ ---
        
        // 1. Получаем ID текущего пользователя
        var currentUserId = _currentUserService.UserId;

        // 2. Если ID не найден (пользователь не залогинен), выбрасываем ошибку
        // (Хотя до этого не дойдет, так как [Authorize] на контроллере его остановит)
        if (currentUserId is null)
        {
            throw new UnauthorizedAccessException();
        }

        // 3. Сравниваем ID владельца продукта с ID текущего пользователя
        if (productEntity.UserId != currentUserId.Value)
        {
            // Если они не совпадают, выбрасываем ForbiddenException
            throw new ForbiddenException("You can only update your own products.");
        }
        // --- ПРОВЕРКА ЗАВЕРШЕНА ---

        _mapper.Map(request.Product, productEntity);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}