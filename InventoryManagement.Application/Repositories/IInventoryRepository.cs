using InventoryManagement.Domain;

namespace InventoryManagement.Application.Repositories;

public interface IInventoryRepository
{
    ICollection<InventoryItem> GetInventoryItems();
}
