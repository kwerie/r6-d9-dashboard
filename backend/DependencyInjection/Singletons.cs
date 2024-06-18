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
        serviceCollection.AddSingleton<Config>(_ =>
        {
            var dbConnectionString = configuration.GetConnectionString("Database") ??
                                     throw new ArgumentException("Missing required config key");
            return new Config
            {
                ConnectionString = dbConnectionString
            };
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