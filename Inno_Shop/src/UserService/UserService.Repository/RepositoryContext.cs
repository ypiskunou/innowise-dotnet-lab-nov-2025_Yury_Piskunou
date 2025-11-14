using Microsoft.EntityFrameworkCore;
using UserService.Entities.Models;
using UserService.Repository.Configuration;

namespace UserService.Repository;

public class RepositoryContext: DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }
    
    public DbSet<User>? Users { get; set; }
    public DbSet<Role>? Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                l => l.HasOne<Role>().WithMany().HasForeignKey("RolesId"),
                r => r.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                j =>
                {
                    j.HasData(
                        new { UsersId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 
                            RolesId = new Guid("c7304655-0810-42f1-8051-710a568b2098") },
                        new { UsersId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 
                            RolesId = new Guid("e13e48f4-754f-4ab1-817a-245152b11379") },
                        new { UsersId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 
                            RolesId = new Guid("e13e48f4-754f-4ab1-817a-245152b11379") }
                    );
                }
            );
    }
}