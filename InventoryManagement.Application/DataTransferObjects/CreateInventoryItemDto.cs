using InventoryManagement.Domain;

namespace InventoryManagement.Application.DataTransferObjects;

public record CreateInventoryItemDto(string Name, string? Description, int Quantity, decimal Price)
{
    public InventoryItem ToInventoryItem()
    {
        return new InventoryItem(Name, Description, Quantity, Price);
    }
}