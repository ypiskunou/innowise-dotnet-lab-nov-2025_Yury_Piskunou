using Microsoft.EntityFrameworkCore;
using UserService.Repository;

namespace UserService.Extensions;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
        try
        {
            appContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR MIGRATING DB: {ex.Message}");
            throw;
        }

        return webApp;
    }
}