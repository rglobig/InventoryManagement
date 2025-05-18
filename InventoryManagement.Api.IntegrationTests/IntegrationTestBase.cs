using InventoryManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace InventoryManagement.Api.IntegrationTests;

public abstract class IntegrationTestBase(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private PostgreSqlContainer _postgreSqlContainer = null!;
    protected HttpClient Client { get; private set; } = null!;

    protected abstract Task SeedDatabase(IServiceProvider serviceProvider);

    private class ConnectionResolverAdapter(string connection) : IConnectionStringResolver
    {
        public string GetConnectionString() => connection;
    }
    
    public async Task InitializeAsync()
    {
        _postgreSqlContainer = new PostgreSqlBuilder().Build();
        await _postgreSqlContainer.StartAsync();

        var connectionString = _postgreSqlContainer.GetConnectionString();
        ConnectionResolverAdapter adapter = new(connectionString);

        var factoryCopy = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.Replace(ServiceDescriptor.Singleton<IConnectionStringResolver>(adapter));
            });
        });

        using var scope = factoryCopy.Services.CreateScope();
        var migration = scope.ServiceProvider.GetRequiredService<IMigrationService>();
        await migration.MigrateAsync();
        
        await SeedDatabase(scope.ServiceProvider);
        
        Client = factoryCopy.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
}
