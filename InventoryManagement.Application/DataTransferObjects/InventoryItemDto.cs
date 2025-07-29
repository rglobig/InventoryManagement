using InventoryManagement.Domain;

namespace InventoryManagement.Application.DataTransferObjects;

public sealed record InventoryItemDto(Guid Id, string Name, string? Description, int Quantity, decimal Price)
{
    public static InventoryItemDto From(InventoryItem item)
    {
        return new InventoryItemDto(item.Id, item.Name, item.Description, item.Quantity, item.Price);
    }
}
