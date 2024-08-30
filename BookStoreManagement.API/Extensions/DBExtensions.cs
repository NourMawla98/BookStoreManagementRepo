using Microsoft.EntityFrameworkCore;

namespace BookStoreManagement.API.Extensions
{
    public static class DBExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost webHost) where T : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<T>();
                if (db.Database.GetPendingMigrations().Any())
                {
                    db.Database.Migrate();
                }
            }
            return webHost;
        }
    }
}