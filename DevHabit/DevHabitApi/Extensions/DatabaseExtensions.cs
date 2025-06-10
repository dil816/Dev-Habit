using DevHabitApi.Database;
using DevHabitApi.Entities;
using Microsoft.AspNetCore.Identity;
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

    public static async Task SeedInitialDataAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        RoleManager<IdentityRole> roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        try
        {
            if (!await roleManager.RoleExistsAsync(Roles.Member))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Member));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occured while seeding initial data");
            throw;
        }
    }
}
