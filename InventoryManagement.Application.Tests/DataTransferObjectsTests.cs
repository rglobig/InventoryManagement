using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Tests;

public class DataTransferObjectsTests
{
    [Fact]
    void CreateInventoryItemDto_ToInventoryItem()
    {
        var input = new CreateInventoryItemDto("iPhone", "Smartphone", 1, 1000);

        var item = input.ToInventoryItem();

        Assert.NotNull(item);
        Assert.Equal(input.Name, item.Name);
        Assert.Equal(input.Description, item.Description);
        Assert.Equal(input.Quantity, item.Quantity);
        Assert.Equal(input.Price, item.Price);
    }

    [Fact]
    void InventoryItemDto_FromInventoryItem()
    {
        var input = new InventoryItem("iPhone", "Smartphone", 1, 1000);

        var item = InventoryItemDto.From(input);

        Assert.NotNull(item);
        Assert.Equal(input.Name, item.Name);
        Assert.Equal(input.Description, item.Description);
        Assert.Equal(input.Quantity, item.Quantity);
        Assert.Equal(input.Price, item.Price);
    }
}
