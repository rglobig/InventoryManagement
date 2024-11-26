using FluentAssertions;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Repositories;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain;
using Moq;

namespace InventoryManagement.Application.Tests;

public class InventoryServiceTests
{
    [Fact]
    void GetInventoryItems_WithOneItem()
    {
        List<InventoryItem> inventory = [new InventoryItem("iPhone", "Smartphone", 1, 1000)];
        var mock = new Mock<IInventoryRepository>();
        mock.Setup(r => r.GetInventoryItems()).Returns(inventory);
        var service = new InventoryService(mock.Object);

        var result = service.GetInventoryItems();

        result.Should().BeEquivalentTo(inventory);
        mock.Verify(r => r.GetInventoryItems(), Times.Once);
    }

    [Fact]
    void GetInventoryItem()
    {
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var id = item.Id;
        var mock = new Mock<IInventoryRepository>();
        mock.Setup(r => r.GetInventoryItem(id)).Returns(item);
        var service = new InventoryService(mock.Object);

        var result = service.GetInventoryItem(id);

        result.Should().BeEquivalentTo(item);
        mock.Verify(r => r.GetInventoryItem(id), Times.Once);
    }

    [Fact]
    void CreateInventoryItem()
    {
        var input = new CreateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        var item = input.ToInventoryItem();

        var mock = new Mock<IInventoryRepository>();
        mock.Setup(r => r.CreateInventoryItem(It.IsAny<InventoryItem>())).Returns(item);
        var service = new InventoryService(mock.Object);

        var result = service.CreateInventoryItem(input);

        result.Should().BeEquivalentTo(item);
        mock.Verify(r => r.CreateInventoryItem(It.IsAny<InventoryItem>()), Times.Once);
    }

    [Fact]
    void TryUpdateInventoryItem_FoundItem()
    {
        var input = new UpdateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var id = item.Id;

        var mock = new Mock<IInventoryRepository>();
        mock.Setup(r => r.GetInventoryItem(id)).Returns(item);
        var service = new InventoryService(mock.Object);

        var result = service.TryUpdateInventoryItem(id, input, out InventoryItem? inventoryItem);

        inventoryItem.Should().NotBeNull();
        inventoryItem.Should().BeEquivalentTo(item);
        result.Should().BeTrue();
    }

    [Fact]
    void TryUpdateInventoryItem_FoundNoItem()
    {
        var input = new UpdateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        InventoryItem item = null!;
        var id = Guid.NewGuid();

        var mock = new Mock<IInventoryRepository>();
        mock.Setup(r => r.GetInventoryItem(id)).Returns(item);
        var service = new InventoryService(mock.Object);

        var result = service.TryUpdateInventoryItem(id, input, out InventoryItem? inventoryItem);

        inventoryItem.Should().BeNull();
        result.Should().BeFalse();
    }

    [Fact]
    void TryDeleteInventoryItem_FoundItem()
    {
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var id = item.Id;

        var mock = new Mock<IInventoryRepository>();
        mock.Setup(r => r.GetInventoryItem(id)).Returns(item);
        var service = new InventoryService(mock.Object);

        var result = service.TryDeleteInventoryItem(id, out InventoryItem? inventoryItem);

        mock.Verify(r => r.DeleteInventoryItem(item), Times.Once);
        inventoryItem.Should().NotBeNull();
        inventoryItem.Should().BeEquivalentTo(item);
        result.Should().BeTrue();
    }

    [Fact]
    void TryDeleteInventoryItem_FoundNoItem()
    {
        InventoryItem item = null!;
        var id = Guid.NewGuid();

        var mock = new Mock<IInventoryRepository>();
        mock.Setup(r => r.GetInventoryItem(id)).Returns(item);
        var service = new InventoryService(mock.Object);

        var result = service.TryDeleteInventoryItem(id, out InventoryItem? inventoryItem);

        mock.Verify(r => r.DeleteInventoryItem(item), Times.Never);
        inventoryItem.Should().BeNull();
        result.Should().BeFalse();
    }

}
