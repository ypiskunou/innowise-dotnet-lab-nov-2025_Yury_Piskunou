using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Entities.Models;

namespace ProductService.Repository.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
        
        builder.HasData(
            new Product
            {
                Id = new Guid("4145bbe3-5e1e-4034-9d54-da63f56c02c3"),
                Name = "Awesome Laptop",
                Description = "A very powerful and modern laptop.",
                Price = 1299.99m,
                IsActive = true,
                IsDeleted = false,
                DateOfCreation = DateTime.UtcNow,
                DateOfLastUpdate = DateTime.UtcNow,
                UserId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Product
            {
                Id = new Guid("560a5138-59b7-43be-aac4-03056c4a27d8"),
                Name = "Ultimate Web API Book",
                Description = "A must-read for all .NET developers.",
                Price = 49.99m,
                IsActive = true,
                IsDeleted = false,
                DateOfCreation = DateTime.UtcNow,
                DateOfLastUpdate = DateTime.UtcNow,
                UserId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                CategoryId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
            }
        );
    }
}