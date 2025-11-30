using Microsoft.EntityFrameworkCore;
using ProductService.Repository;

namespace ProductService.Extensions;

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