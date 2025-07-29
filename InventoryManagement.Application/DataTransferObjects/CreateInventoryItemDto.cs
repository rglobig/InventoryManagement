using InventoryManagement.Domain;

namespace InventoryManagement.Application.DataTransferObjects;

public sealed record CreateInventoryItemDto(string Name, string? Description, int Quantity, decimal Price)
{
    public InventoryItem ToInventoryItem()
    {
        return new InventoryItem(Name, Description, Quantity, Price);
    }
}