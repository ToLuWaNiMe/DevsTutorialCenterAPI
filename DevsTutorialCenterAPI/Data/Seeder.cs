using DevsTutorialCenterAPI.Data.Entities;
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

        if (!context.Tenants.Any())
        {
            context.AddAsync(new Tenant { 
                Id = "1", 
                Name = "Devs Tutorial Center MVC", 
                Identity = "devstutorialcenter@gmail.com", 
                Password = "jkl;.!fsergrs;;=__",
                UpdatedOn = DateTime.Now, 
                CreatedOn = DateTime.Now 
            });
        }
    }
}