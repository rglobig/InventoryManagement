using InventoryManagement.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace InventoryManagement.Application;

internal sealed class EnvironmentConnectionStringResolver(IConfiguration configuration) : IConnectionStringResolver
{
    public string GetConnectionString()
    {
        var host = configuration["DB_HOST"];
        var port = configuration["DB_PORT"];
        var username = configuration["DB_USERNAME"];
        var password = configuration["DB_PASSWORD"];
        var name = configuration["DB_NAME"];
        var defaultConnection = $"Host={host};Port={port};UserName={username};Password={password};Database={name}";
        return defaultConnection;
    }
}