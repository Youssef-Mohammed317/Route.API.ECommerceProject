using E_Commerce.Domian.Interfaces;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Api.Extensions
{
    public static class WebApplicationResgistration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync<TDbContext>(this WebApplication app) where TDbContext : DbContext
        {
            using var scope = app.Services.CreateAsyncScope();

            var storeDbContext = scope.ServiceProvider.GetService<TDbContext>();

            if ((await storeDbContext?.Database.GetPendingMigrationsAsync()!).Any())
            {
                await storeDbContext.Database.MigrateAsync();
            }

            return app;
        }
        public static async Task<WebApplication> SeedDataAsync(this WebApplication app, string key = "default")
        {
            using var scope = app.Services.CreateAsyncScope();

            var DataInitializer = scope.ServiceProvider.GetRequiredKeyedService<IDataInitializer>(key);

            await DataInitializer?.InitializeAsync()!;

            return app;
        }
    }
}
