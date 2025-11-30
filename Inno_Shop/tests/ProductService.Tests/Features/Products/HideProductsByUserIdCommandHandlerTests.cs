using Moq;
using ProductService.Application.Features.Products.HideProductsByUserId;
using ProductService.Contracts;
using ProductService.Entities.Models;

namespace ProductService.Tests.Features.Products;

public class HideProductsByUserIdCommandHandlerTests
{
    private readonly Mock<IRepositoryManager> _mockRepo;
    private readonly Mock<IProductRepository> _mockProductRepo;
    private readonly HideProductsByUserIdCommandHandler _handler;

    public HideProductsByUserIdCommandHandlerTests()
    {
        _mockRepo = new Mock<IRepositoryManager>();
        _mockProductRepo = new Mock<IProductRepository>();
        
        _mockRepo.Setup(x => x.Product).Returns(_mockProductRepo.Object);

        _handler = new HideProductsByUserIdCommandHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_Should_SetIsDeletedTrue_For_AllUserProducts()
    {
        var userId = Guid.NewGuid();
        var command = new HideProductsByUserIdCommand(userId);
        
        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "P1", Price = 500, UserId = userId, IsDeleted = false },
            new Product { Id = Guid.NewGuid(), Name = "P2", Price = 50, UserId = userId, IsDeleted = false }
        };
        
        _mockProductRepo.Setup(r => 
                r.GetProductsByUserIdAsync(userId, true, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);
        
        await _handler.Handle(command, CancellationToken.None);
        
        Assert.True(products[0].IsDeleted);
        Assert.True(products[1].IsDeleted);
        
        _mockRepo.Verify(r => 
            r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}