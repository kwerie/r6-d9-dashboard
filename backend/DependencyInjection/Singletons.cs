using backend.Configuration;
using backend.Setup;

namespace backend.DependencyInjection;

public static class Singletons
{
    public static void SetupSingletons(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<Config>(_ => new Config
        {
            connectionString = "server=localhost;port=3306;database=local;user=local;password=local"
        });
        serviceCollection.AddDbContext<DashboardDbContext>();
    }
}