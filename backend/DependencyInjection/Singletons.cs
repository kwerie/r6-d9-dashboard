using backend.Client;
using backend.Configuration;
using backend.Repositories;
using backend.Services;
using backend.Setup;

namespace backend.DependencyInjection;

public static class Singletons
{
    public static void SetupSingletons(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<Config>(_ => new Config
        {
            ConnectionString = "server=database;port=3306;database=local;user=local;password=local"
            // ConnectionString = "server=localhost;port=3306;database=local;user=local;password=local" // TODO: get connection string from config
        });
        serviceCollection.AddDbContext<DashboardDbContext>();
        serviceCollection.AddSingleton<HttpClient>(_ => new HttpClient());
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IDiscordLoginSessionService, DiscordLoginSessionService>();
        serviceCollection.AddScoped<IDiscordLoginSessionRepository, DiscordLoginSessionRepository>();
        serviceCollection.AddScoped<ILoginSessionRepository, LoginSessionRepository>();
        serviceCollection.AddScoped<ILoginSessionService, LoginSessionService>();
        serviceCollection.AddScoped<IJwtService, JwtService>(_ => new JwtService(
            configuration.GetValue<string?>("Jwt:Key", null) ?? throw new InvalidOperationException("Missing config key Jwt:Key"),
            configuration.GetValue<string?>("Jwt:Issuer", null) ?? throw new InvalidOperationException("Missing config key Jwt:Issuer"),
            configuration.GetValue<string?>("Jwt:Audience", null) ?? throw new InvalidOperationException("Missing config key Jwt:Audience")
        ));
        serviceCollection.AddSingleton<IDiscordClient, DiscordClient>(services =>
        {
            var config = configuration.GetSection("Discord");
            return new DiscordClient(
                services.GetRequiredService<HttpClient>(),
                config.GetValue<string?>("ClientId", null) ?? throw new InvalidOperationException("Missing config key ClientId"),
                config.GetValue<string?>("ClientSecret", null) ?? throw new InvalidOperationException("Missing config key ClientSecret"),
                config.GetValue<string?>("RedirectUri", null) ?? throw new InvalidOperationException("Missing config key RedirectUri")
            );
        });
    }
}