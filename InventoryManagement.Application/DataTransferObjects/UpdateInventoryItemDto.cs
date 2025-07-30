using InventoryManagement.Domain;

namespace InventoryManagement.Application.DataTransferObjects;

public sealed record UpdateInventoryItemDto(string? Name, string? Description, int? Quantity, decimal? Price)
{
    public static UpdateInventoryItemDto From(InventoryItem item)
    {
        return new UpdateInventoryItemDto(item.Name, item.Description, item.Quantity, item.Price);
    }
}