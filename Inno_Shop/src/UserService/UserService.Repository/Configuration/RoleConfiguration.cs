using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Entities.Models;

namespace UserService.Repository.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = new Guid("c7304655-0810-42f1-8051-710a568b2098"),
                Name = "Admin"
            },
            new Role
            {
                Id = new Guid("e13e48f4-754f-4ab1-817a-245152b11379"),
                Name = "User"
            }
        );
    }
}