namespace UserService.Contracts;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    IRoleRepository Role { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}