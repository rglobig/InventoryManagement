using InventoryManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Application;

public static class Dependencies
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IInventoryService, InventoryService>();
    }
}
