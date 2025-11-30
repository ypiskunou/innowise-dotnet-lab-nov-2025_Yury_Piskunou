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
        
        builder.HasIndex(p => p.CategoryId).HasDatabaseName("IX_Products_CategoryId");
        builder.HasIndex(p => p.UserId).HasDatabaseName("IX_Products_UserId");
        builder.HasIndex(p => p.Name);
        builder.HasQueryFilter(p => !p.IsDeleted && p.IsActive);
        builder.Navigation(p => p.Category).AutoInclude();

        builder.HasData(
            new Product
            {
                Id = new Guid("8b007a4c-b9d0-4508-b233-ba47ffeb185f"),
                Name = "Asus K501-UW",
                Description = "Modern cut edge powerfull notebook",
                Price = 1000m,
                IsActive = true,
                IsDeleted = false,
                DateOfCreation = DateTime.Parse("2025-11-29T19:34:28.328986Z").ToUniversalTime(),
                UserId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Product
            {
                Id = new Guid("6a1a626b-8b48-42c5-8107-99a2a0e517fe"),
                Name = "Samsung RV513",
                Description = "Old low budget notebook",
                Price = 100m,
                IsActive = true,
                IsDeleted = false,
                DateOfCreation = DateTime.Parse("2025-11-29T19:36:23.966282Z").ToUniversalTime(),
                UserId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Product
            {
                Id = new Guid("4145bbe3-5e1e-4034-9d54-da63f56c02c3"),
                Name = "Awesome Laptop",
                Description = "A very powerful and modern laptop.",
                Price = 1299.99m,
                IsActive = true,
                IsDeleted = false,
                DateOfCreation = DateTime.Parse("2025-11-30T01:22:06.517691Z").ToUniversalTime(),
                UserId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 
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
                DateOfCreation = DateTime.Parse("2025-11-30T01:22:06.517692Z").ToUniversalTime(),
                UserId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                CategoryId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
            },
            new Product
            {
                Id = new Guid("76612c72-c934-4a97-8195-78a85badb03e"),
                Name = "Test Phone",
                Description = "Test desc",
                Price = 100m,
                IsActive = true,
                IsDeleted = true, 
                DateOfCreation = DateTime.Parse("2025-11-24T04:57:27.431701Z").ToUniversalTime(),
                UserId = new Guid("f2b8dd4e-bb69-41ce-8fd6-f213790a460b"), 
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            }
        );
    }
}