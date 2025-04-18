using DevHabitApi.Database;
using Microsoft.EntityFrameworkCore;

namespace DevHabitApi.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        await using ApplicationDbContext ApplicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await using ApplicationIdentityDbContext IdentityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

        try
        {
            await ApplicationDbContext.Database.MigrateAsync();
            app.Logger.LogInformation("Application Database migration applied successfully.");

            await IdentityDbContext.Database.MigrateAsync();
            app.Logger.LogInformation("Identity Database migration applied successfully.");
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "An error occured while applying databse migrations");
            throw;
        }
    }
}
