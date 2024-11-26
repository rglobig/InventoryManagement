using InventoryManagement.Domain;

namespace InventoryManagement.Application.Services;

public interface IInventoryService
{
    ICollection<InventoryItem> GetInventoryItems();
}
