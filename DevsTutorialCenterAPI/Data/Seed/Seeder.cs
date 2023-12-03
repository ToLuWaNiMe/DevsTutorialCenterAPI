using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Data;

public static class Seeder
{
    public static async Task Seed(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider
            .GetRequiredService<DevsTutorialCenterAPIContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
            await context.Database.MigrateAsync();

        if (!context.AppUsers.Any())
        {
            var seeder = new SeedData(context);
            await seeder.Run();
        }
    }
}