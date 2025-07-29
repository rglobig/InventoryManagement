using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure;

internal sealed class MigrationService(InventoryDbContext dbContext) : IMigrationService
{
    public async Task MigrateAsync()
    {
        await dbContext.Database.MigrateAsync();
    }
}