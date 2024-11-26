using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Repositories;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Services;

public class InventoryService(IInventoryRepository inventoryRepository) : IInventoryService
{
    public ICollection<InventoryItem> GetInventoryItems()
    {
        return inventoryRepository.GetInventoryItems();
    }

    public InventoryItem? GetInventoryItem(Guid id)
    {
        return inventoryRepository.GetInventoryItem(id);
    }

    public InventoryItem CreateInventoryItem(CreateInventoryItemDto data)
    {
        return inventoryRepository.CreateInventoryItem(data.ToInventoryItem());
    }
}
