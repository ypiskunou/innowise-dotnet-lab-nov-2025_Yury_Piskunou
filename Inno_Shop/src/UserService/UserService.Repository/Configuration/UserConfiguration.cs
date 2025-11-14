using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Entities.Models;

namespace UserService.Repository.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Admin User",
                Email = "admin@example.com",
                PasswordHash = "HASHED_ADMIN_PASSWORD"
            },
            new User
            {
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                Name = "Regular User",
                Email = "user@example.com",
                PasswordHash = "HASHED_USER_PASSWORD"
            }
        );
    }
}