using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;

namespace Tournament.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TournamentApiContext>();

                // Uncomment these two for a fresh database installation with SeedData.
                // await context.Database.EnsureDeletedAsync();
                // await context.Database.MigrateAsync();

                if (await context.Tournaments.AnyAsync()) return;

                try
                {
                    await SeedData.Init(context);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
