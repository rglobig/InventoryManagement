using InventoryManagement.Application.Repositories;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Services;

public class InventoryService(IInventoryRepository inventoryRepository) : IInventoryService
{
    public ICollection<InventoryItem> GetInventoryItems()
    {
        return inventoryRepository.GetInventoryItems();
    }
}
