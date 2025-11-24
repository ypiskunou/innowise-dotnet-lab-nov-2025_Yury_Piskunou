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
                PasswordHash = "AQAAAAIAAYagAAAAEIIzHBkWv7JytQET6e5qtoNMQpXngwmkQwoOru9yImtfeGzabBmFk0gK3Tpq5sgogg"
            },
            new User
            {
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                Name = "Regular User",
                Email = "user@example.com",
                PasswordHash = "AQAAAAIAAYagAAAAEHxYENv88wq8F6SIbaWKZMBOM+FdQlbzej9N0xNpX5I8fHdSUH+Lv1sEWe7EJxhe5Q=="
            }
        );
    }
}