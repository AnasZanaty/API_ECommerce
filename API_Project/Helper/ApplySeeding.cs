using Core;
using Core.Identity;
using Core.IdentityEntities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.Helper
{
    public class ApplySeeding
    {

        public static async Task ApplySeedingAsync( WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<StoreDbContext>();
                    var IdentityContext = services.GetRequiredService<AppIdentityDbContext>();
                    var userManger = services.GetRequiredService<UserManager< AppUser >> ();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedASync(context, loggerFactory);
                    await AppIdentityContextSeed.SeedUserAsync(userManger);

                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                    logger.LogError(ex.Message);
                }

            }
        }
    }
}
