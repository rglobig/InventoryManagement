using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure;

public class MigrationService(InventoryDbContext dbContext) : IMigrationService
{
    public async Task MigrateAsync()
    {
        await dbContext.Database.MigrateAsync();
    }
}
