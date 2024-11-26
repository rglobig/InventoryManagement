using InventoryManagement.Application.Repositories;
using InventoryManagement.Domain;

namespace InventoryManagement.Infrastructure;

public class InventoryRepository(InventoryDbContext dbContext) : IInventoryRepository
{
    public InventoryItem? GetInventoryItem(Guid id)
    {
        return dbContext.InventoryItems.Find(id);
    }

    public ICollection<InventoryItem> GetInventoryItems()
    {
        return dbContext.InventoryItems.OrderBy(item => item.Name).ToList();
    }
}
