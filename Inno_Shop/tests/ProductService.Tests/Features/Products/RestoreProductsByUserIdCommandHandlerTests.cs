using Moq;
using ProductService.Application.Features.Products.RestoreProductsByUserId;
using ProductService.Contracts;
using ProductService.Entities.Models;

namespace ProductService.Tests.Features.Products;

public class RestoreProductsByUserIdCommandHandlerTests
{
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly Mock<IProductRepository> _mockProductRepo;
    private readonly RestoreProductsByUserIdCommandHandler _handler;

    public RestoreProductsByUserIdCommandHandlerTests()
    {
        _mockRepo = new Mock<IRepositoryManager>();
        _mockProductRepo = new Mock<IProductRepository>();
        
        _mockRepo.Setup(x => x.Product).Returns(_mockProductRepo.Object);

        _handler = new RestoreProductsByUserIdCommandHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_Should_SetIsDeletedFalse_For_AllUserProducts()
    {
        var userId = Guid.NewGuid();
        var command = new RestoreProductsByUserIdCommand(userId);
        
        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "P1", Price = 100, UserId = userId, IsDeleted = true },
            new Product { Id = Guid.NewGuid(), Name = "P2", Price = 500, UserId = userId, IsDeleted = true }
        };
        
        _mockProductRepo.Setup(r => r.GetProductsByUserIdAsync(
                userId, 
                true,
                true,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);
        
        await _handler.Handle(command, CancellationToken.None);
        
        Assert.False(products[0].IsDeleted);
        Assert.False(products[1].IsDeleted);
        
        _mockRepo.Verify(r => 
            r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}