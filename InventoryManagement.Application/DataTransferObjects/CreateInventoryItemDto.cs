﻿using InventoryManagement.Domain;

namespace InventoryManagement.Application.DataTransferObjects;

public record CreateInventoryItemDto(string Name, string? Description, int Quantity, decimal Price)
{
    public InventoryItem ToInventoryItem() => new(Name, Description, Quantity, Price);
}
