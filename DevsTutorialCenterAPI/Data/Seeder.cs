using Microsoft.EntityFrameworkCore;

namespace DevsTutorialCenterAPI.Data;

public static class Seeder
{
    public static void SeedeMe(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider
            .GetRequiredService<DevsTutorialCenterAPIContext>();
        if (context.Database.GetPendingMigrations().Any()) context.Database.MigrateAsync().Wait();

        if (!context.Articles.Any())
        {
            var articleData = SeedData.Articles;
            context.AddRangeAsync(articleData).Wait();
            context.SaveChangesAsync().Wait();
        }
    }
}