using InventoryManagement.Application.Repositories;
using InventoryManagement.Domain;

namespace InventoryManagement.Infrastructure;

public class InventoryRepository(InventoryDbContext dbContext) : IInventoryRepository
{
    public InventoryItem CreateInventoryItem(InventoryItem item)
    {
        var entity = dbContext.InventoryItems.Add(item);
        dbContext.SaveChanges();
        return entity.Entity;
    }

    public InventoryItem? GetInventoryItem(Guid id)
    {
        return dbContext.InventoryItems.Find(id);
    }

    public ICollection<InventoryItem> GetInventoryItems()
    {
        return dbContext.InventoryItems.OrderBy(item => item.Name).ToList();
    }
}
