using FluentAssertions;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Tests;

public class DataTransferObjectsTests
{
    private readonly CreateInventoryItemDto _createDto = new("iPhone", "Smartphone", 1, 1000);
    private readonly InventoryItem _item = new("iPhone", "Smartphone", 1, 1000);

    [Fact]
    private void CreateInventoryItemDto_To_InventoryItem()
    {
        var item = _createDto.ToInventoryItem();

        item.Should().BeEquivalentTo(_createDto);
    }

    [Fact]
    private void CreateInventoryItemDto_From_InventoryItem()
    {
        var dto = CreateInventoryItemDto.From(_item);

        dto.Should().BeEquivalentTo(_item, options =>
            options.Excluding(x => x.Id)
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt));
    }

    [Fact]
    private void InventoryItemDto_From_InventoryItem()
    {
        var dto = InventoryItemDto.From(_item);

        dto.Should().BeEquivalentTo(_item, options =>
            options.Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt));
    }

    [Fact]
    private void UpdateInventoryItemDto_From_InventoryItem()
    {
        var dto = UpdateInventoryItemDto.From(_item);

        dto.Should().BeEquivalentTo(_item, options =>
            options.Excluding(x => x.Id)
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt));
    }
}
