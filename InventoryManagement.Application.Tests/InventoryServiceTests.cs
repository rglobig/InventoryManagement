using FluentAssertions;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain;
using Moq;

namespace InventoryManagement.Application.Tests;

public class InventoryServiceTests
{
    [Fact]
    async Task GetInventoryItems_WithOneItem()
    {
        List<InventoryItem> inventory = [new InventoryItem("iPhone", "Smartphone", 1, 1000)];
        var mock = new Mock<IInventoryRepository>();
        var token = new CancellationToken();
        mock.Setup(r => r.GetInventoryItems(token)).ReturnsAsync(inventory);
        var service = new InventoryService(mock.Object);

        var result = await service.GetInventoryItems(token);

        result.Should().BeEquivalentTo(inventory);
        mock.Verify(r => r.GetInventoryItems(token), Times.Once);
    }

    [Fact]
    async Task GetInventoryItem()
    {
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var id = item.Id;
        var mock = new Mock<IInventoryRepository>();
        var token = new CancellationToken();
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.GetInventoryItem(id, token);

        result.Should().BeEquivalentTo(item);
        mock.Verify(r => r.GetInventoryItem(id, token), Times.Once);
    }

    [Fact]
    async Task CreateInventoryItem()
    {
        var input = new CreateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        var item = input.ToInventoryItem();

        var mock = new Mock<IInventoryRepository>();
        var token = new CancellationToken();
        mock.Setup(r => r.CreateInventoryItem(It.IsAny<InventoryItem>(), token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.CreateInventoryItem(input, token);
        result.Should().BeEquivalentTo(item);
        mock.Verify(r => r.CreateInventoryItem(It.IsAny<InventoryItem>(), token), Times.Once);
    }

    [Fact]
    async Task TryUpdateInventoryItem_FoundItem()
    {
        var input = new UpdateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var id = item.Id;

        var mock = new Mock<IInventoryRepository>();
        var token = new CancellationToken();
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.UpdateInventoryItem(id, input, token);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(item);
    }

    [Fact]
    async Task TryUpdateInventoryItem_FoundNoItem()
    {
        var input = new UpdateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        InventoryItem item = null!;
        var id = Guid.NewGuid();

        var mock = new Mock<IInventoryRepository>();
        var token = new CancellationToken();
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.UpdateInventoryItem(id, input, token);

        result.Should().BeNull();
    }

    [Fact]
    async Task TryDeleteInventoryItem_FoundItem()
    {
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var id = item.Id;

        var mock = new Mock<IInventoryRepository>();
        var token = new CancellationToken();
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.DeleteInventoryItem(id, token);

        mock.Verify(r => r.DeleteInventoryItem(item, token), Times.Once);
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(item);
    }

    [Fact]
    async Task TryDeleteInventoryItem_FoundNoItem()
    {
        InventoryItem item = null!;
        var id = Guid.NewGuid();

        var mock = new Mock<IInventoryRepository>();
        var token = new CancellationToken();
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.DeleteInventoryItem(id, token);

        mock.Verify(r => r.DeleteInventoryItem(item, token), Times.Never);
        result.Should().BeNull();
    }

}
