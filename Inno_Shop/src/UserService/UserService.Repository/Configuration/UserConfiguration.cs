using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Entities.Models;

namespace UserService.Repository.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Email)
            .IsUnique(); 
        
        builder.HasIndex(u => u.Name);
        
        builder.HasData(
            new User
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Admin User",
                Email = "admin@example.com",
                PasswordHash = "AQAAAAIAAYagAAAAEGtVOXkmwwc57KzDGVK1Y8M8GCICFLFvDpaDilkYvr48jPPbt1vOxyTeCUCD0s6p+Q==",
                VerifiedAt = DateTime.UtcNow, 
                IsActive = true
            },
            new User
            {
                Id = new Guid("84fe1970-e510-45c3-90a8-8e0607d056e9"),
                Name = "Regular User",
                Email = "user@example.com",
                PasswordHash = "AQAAAAIAAYagAAAAEHP8SZ2ACcbxZChtJd4YrJW3H1hfZYbUpWAeifxw6heZoZpI9PJcs+Ks5S0ul4ryCQ==",
                VerifiedAt = DateTime.UtcNow,
                IsActive = true
            },
            new User
            {
                Id = new Guid("f2b8dd4e-bb69-41ce-8fd6-f213790a460b"),
                Name = "Anton Volchkov",
                Email = "volchkov@inno.by",
                PasswordHash = "AQAAAAIAAYagAAAAEGzGIA5pP3SUaaSHYI093ON4tjYMcLdcY3MDAXB0CwtQIENCmU8jSxEqRfc4ze8XUg==",
                VerifiedAt = DateTime.UtcNow,
                IsActive = false 
            }
        );
    }
}