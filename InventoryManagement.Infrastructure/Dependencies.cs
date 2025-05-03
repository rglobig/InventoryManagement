using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure;

public static class Dependencies
{
    public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddDbContext<InventoryDbContext>(UseNpgsql(configuration));
    }
        
    private static Action<DbContextOptionsBuilder> UseNpgsql(IConfiguration configuration)
    {
        var host = configuration["DB_HOST"];
        var port = configuration["DB_PORT"];
        var username = configuration["DB_USERNAME"];
        var password = configuration["DB_PASSWORD"];
        var name = configuration["DB_NAME"];
        var defaultConnection = $"Host={host};Port={port};UserName={username};Password={password};Database={name}";
        return options => options.UseNpgsql(defaultConnection);
    }
}
