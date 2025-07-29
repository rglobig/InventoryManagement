using InventoryManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace InventoryManagement.Api.IntegrationTests;

public abstract class IntegrationTestBase(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private HttpClient _client = null!;
    private PostgreSqlContainer _postgreSqlContainer = null!;

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

        _client = factoryCopy.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }

    protected async Task<HttpResponseMessage> GetAsync(string url)
    {
        return await _client.GetAsync(url);
    }

    protected async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
    {
        return await _client.PostAsJsonAsync(url, data);
    }

    protected async Task<HttpResponseMessage> PatchAsync<T>(string url, T data)
    {
        return await _client.PatchAsJsonAsync(url, data);
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string url)
    {
        return await _client.DeleteAsync(url);
    }

    protected abstract Task SeedDatabase(IServiceProvider serviceProvider);

    private class ConnectionResolverAdapter(string connection) : IConnectionStringResolver
    {
        public string GetConnectionString()
        {
            return connection;
        }
    }
}
