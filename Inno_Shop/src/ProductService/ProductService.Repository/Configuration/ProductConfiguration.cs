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
                DateOfLastUpdate = DateTime.Parse("2025-11-29T19:34:28.328983Z").ToUniversalTime(),
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
                DateOfLastUpdate = DateTime.Parse("2025-11-29T19:36:23.966280Z").ToUniversalTime(),
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
                DateOfLastUpdate = DateTime.Parse("2025-11-30T01:22:06.517691Z").ToUniversalTime(),
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
                DateOfCreation = DateTime.Parse("2025-11-30T01:22:06.517692Z").ToUniversalTime(),
                DateOfLastUpdate = DateTime.Parse("2025-11-30T01:22:06.517692Z").ToUniversalTime(),
                UserId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 
                CategoryId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
            },
            new Product
            {
                Id = new Guid("5dae7ef3-f812-457c-b8a4-49cfa9d9fd2a"),
                Name = "Yotaphone 2",
                Description = "Два прекрасных экрана по цене одного. AMOLED даёт чёткое, яркое и насыщенное изображение, а E-Ink экономит заряд вашего телефона и позволяет с комфортом читать с него при ярком солнечном свете.",
                Price = 110m,
                IsActive = true,
                IsDeleted = false,
                DateOfCreation = DateTime.Parse("2025-12-01T21:22:04.997101Z").ToUniversalTime(),
                DateOfLastUpdate = DateTime.Parse("2025-12-01T21:22:04.997098Z").ToUniversalTime(),
                UserId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") 
            },
            new Product
            {
                Id = new Guid("b3774de9-7aa9-447a-91e0-68bad89813d8"),
                Name = "Я робот - Айзек Азимов",
                Description = "Классика жанра сай-фай. По этому произведению был снят одноименный фильм. А законы робототехники вот-вот найдут применение.",
                Price = 49m,
                IsActive = true,
                IsDeleted = false,
                DateOfCreation = DateTime.Parse("2025-12-07T21:08:41.834447Z").ToUniversalTime(),
                DateOfLastUpdate = DateTime.Parse("2025-12-07T21:08:41.834445Z").ToUniversalTime(),
                UserId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 
                CategoryId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") 
            },
            new Product
            {
                Id = new Guid("f8e433ff-f6db-4cf5-aa29-a1db7fd9bd18"),
                Name = "Сад богов - Джеральд Даррелл",
                Description = "Прекрасная автобиографическая книга о солнечном детстве писателя на греческом острове Корфу. Одна из трилогии.",
                Price = 150m,
                IsActive = true,
                IsDeleted = false,
                DateOfCreation = DateTime.Parse("2025-12-01T00:50:48.229130Z").ToUniversalTime(),
                DateOfLastUpdate = DateTime.Parse("2025-12-01T00:50:48.229128Z").ToUniversalTime(),
                UserId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                CategoryId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") 
            },
            new Product
            {
                Id = new Guid("d13254ef-108e-4a22-9c15-f70e7479898c"),
                Name = "Kindle E-Ink Book",
                Description = "Электронный гаджет на основе технологии E-Ink. Предназначен для комфортного чтения электронных книг. У него экономичный режим энергопотребления, которого хватает на неделю работы. Возьми с собой всего Гарри Поттера!",
                Price = 350m,
                IsActive = true,
                IsDeleted = false,
                DateOfCreation = DateTime.Parse("2025-12-07T21:29:11.387536Z").ToUniversalTime(),
                DateOfLastUpdate = DateTime.Parse("2025-12-07T21:29:11.387535Z").ToUniversalTime(),
                UserId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") 
            },
            new Product
            {
                Id = new Guid("3e03a29f-7328-46be-8475-3d0c93e1d29c"),
                Name = "MacBook Air",
                Description = "Легкий мощный ноутбук",
                Price = 1200m,
                IsActive = true,
                IsDeleted = true, 
                DateOfCreation = DateTime.Parse("2025-12-01T03:04:47.796790Z").ToUniversalTime(),
                DateOfLastUpdate = DateTime.Parse("2025-12-01T03:04:47.796787Z").ToUniversalTime(),
                UserId = new Guid("f2b8dd4e-bb69-41ce-8fd6-f213790a460b"), 
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") 
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
                DateOfLastUpdate = DateTime.Parse("2025-11-24T04:57:27.431698Z").ToUniversalTime(),
                UserId = new Guid("f2b8dd4e-bb69-41ce-8fd6-f213790a460b"),
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") 
            }
        );
    }
}