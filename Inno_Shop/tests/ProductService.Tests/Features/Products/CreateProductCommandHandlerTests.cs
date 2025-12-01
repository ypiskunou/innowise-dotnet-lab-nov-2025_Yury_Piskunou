using AutoMapper;
using FluentAssertions;
using Moq;
using ProductService.Application.Features.Products.CreateProduct;
using ProductService.Contracts;
using ProductService.Entities.Models;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Tests.Features.Products;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IRepositoryManager> _mockRepoManager;
    
    private readonly Mock<IProductRepository> _mockProductRepo;
    private readonly Mock<ICategoryRepository> _mockCategoryRepo;
    
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ICurrentUserService> _mockUser;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        _mockRepoManager = new Mock<IRepositoryManager>();
        _mockProductRepo = new Mock<IProductRepository>(); 
        _mockCategoryRepo = new Mock<ICategoryRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockUser = new Mock<ICurrentUserService>();
        
        _mockRepoManager.Setup(x => x.Product).Returns(_mockProductRepo.Object);
        _mockRepoManager.Setup(x => x.Category).Returns(_mockCategoryRepo.Object);
        
        _handler = new CreateProductCommandHandler(_mockRepoManager.Object, _mockMapper.Object, _mockUser.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnProductDto_When_DataIsValid()
    {
        var command = new CreateProductCommand(new ProductForCreationDto("Test Product", 100, "Desc", Guid.NewGuid()));
        var userId = Guid.NewGuid();
        var category = new Category { Id = command.Product.CategoryId, Name = "Cat" };
        
        var productEntity = new Product 
        { 
            Id = Guid.NewGuid(), 
            Name = "Test Product", 
            Price = 100,
            UserId = userId,
        };
        
        var productDto = new ProductDto(
            productEntity.Id, 
            "Test Product", 
            100, 
            "Desc", 
            "Cat",
            Guid.NewGuid(), 
            true
            );

        
        _mockUser.Setup(x => x.UserId).Returns(userId);
        
        _mockCategoryRepo.Setup(r => r.GetCategoryByIdAsync(It.IsAny<Guid>(), false, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        _mockMapper.Setup(m => m.Map<Product>(It.IsAny<ProductForCreationDto>()))
            .Returns(productEntity);
            
        _mockMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Product>()))
            .Returns(productDto);
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        result.Should().NotBeNull();
        result.Name.Should().Be("Test Product");
        
        _mockProductRepo.Verify(x => x.CreateProduct(It.IsAny<Product>()), Times.Once);
        _mockRepoManager.Verify(x => x
            .SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}