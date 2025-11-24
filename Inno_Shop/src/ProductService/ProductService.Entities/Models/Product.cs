
namespace ProductService.Entities.Models;

public class Product
{
    public Guid Id { get; init; }
    public DateTime DateOfCreation { get; set; }
    public DateTime DateOfLastUpdate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public required string Name { get; set; }
    public string? Description { get; set; } 
    public required decimal Price { get; set; }
    public required Guid UserId { get; set; } 
    public string? ImageUrl { get; set; } 
    public string? Location { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}