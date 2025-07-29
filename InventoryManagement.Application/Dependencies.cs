using InventoryManagement.Application.DataTransferObjects.Validation;
using InventoryManagement.Application.Services;
using InventoryManagement.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Application;

public static class Dependencies
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionStringResolver, EnvironmentConnectionStringResolver>();
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<CreateInventoryItemDtoValidator>();
        services.AddScoped<UpdateInventoryItemDtoValidator>();
    }
}
