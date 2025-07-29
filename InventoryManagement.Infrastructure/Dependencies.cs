using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure;

public static class Dependencies
{
    public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMigrationService, MigrationService>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddDbContext<InventoryDbContext>(UseNpgsql);
    }

    private static void UseNpgsql(IServiceProvider provider, DbContextOptionsBuilder options)
    {
        var resolver = provider.GetRequiredService<IConnectionStringResolver>();
        options.UseNpgsql(resolver.GetConnectionString());
    }
}