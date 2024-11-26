using InventoryManagement.Domain;

namespace InventoryManagement.Application.Repositories;

public interface IInventoryRepository
{
    ICollection<InventoryItem> GetInventoryItems();
    InventoryItem? GetInventoryItem(Guid id);
    InventoryItem CreateInventoryItem(InventoryItem item);
}
