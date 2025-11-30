
namespace UserService.Entities.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    
    public bool IsActive { get; set; } = true;
    public string? VerificationToken { get; set; }
    public DateTime? VerifiedAt { get; set; }
    
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}