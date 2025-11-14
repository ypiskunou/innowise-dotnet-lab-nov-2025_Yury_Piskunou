using Repository;
using UserService.Contracts;

namespace UserService.Repository;

public class RepositoryManager: IRepositoryManager
{
    private readonly RepositoryContext _context;
    
    private readonly Lazy<IUserRepository> _author;
    private readonly Lazy<IRoleRepository> _book;

    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
        _author = new Lazy<IUserRepository>(() => new UserRepository(context));
        _book = new Lazy<IRoleRepository>(() => new RoleRepository(context));
    }
    
    public IUserRepository User => _author.Value;
    public IRoleRepository Role => _book.Value;
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => 
        await _context.SaveChangesAsync(cancellationToken);
}