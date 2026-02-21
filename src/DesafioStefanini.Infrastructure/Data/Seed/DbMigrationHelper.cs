using DesafioStefanini.Infrastructure.Data.Contexts;
using DesafioStefanini.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DesafioStefanini.Infrastructure.Seed;

public static class DbMigrationHelper
{
    public static async Task UseDbInitializationAsync(this IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                // Pega os dois contextos
                var appContext = services.GetRequiredService<ApplicationDbContext>();

                // Aplica migrations de ambos
                await appContext.Database.MigrateAsync();

                // Seed de Negócio
                await DbInitializer.SeedAsync(appContext);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("Seed");
                logger.LogError(ex, "Ocorreu um erro ao inicializar o Banco de Dados.");
            }
        }
    }
}