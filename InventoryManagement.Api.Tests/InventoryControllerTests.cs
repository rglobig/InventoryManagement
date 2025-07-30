using InventoryManagement.Api.Controllers;
using InventoryManagement.Application;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain;
using Microsoft.AspNetCore.Http;
using Moq;

namespace InventoryManagement.Api.Tests;

public class InventoryControllerTests
{
    private static readonly InventoryItem DefaultItem = new("iPhone", "Smartphone", 1, 1000);
    private readonly InventoryController _controller;
    private readonly CreateInventoryItemDto _createInventoryItemDto = CreateInventoryItemDto.From(DefaultItem);
    private readonly List<InventoryItem> _inventory = [DefaultItem];
    private readonly Mock<IInventoryService> _inventoryServiceMock;
    private readonly UpdateInventoryItemDto _updateDto = UpdateInventoryItemDto.From(DefaultItem);
    private readonly Mock<IUriProvider> _uriProviderMock;

    public InventoryControllerTests()
    {
        _inventoryServiceMock = new Mock<IInventoryService>();
        _uriProviderMock = new Mock<IUriProvider>();
        _controller = new InventoryController(_inventoryServiceMock.Object, _uriProviderMock.Object);
    }

    private static InventoryItemDto DefaultItemDto => InventoryItemDto.From(DefaultItem);
    private IReadOnlyList<InventoryItemDto> InventoryDtos => _inventory.Select(InventoryItemDto.From).ToList();

    [Fact]
    private async Task Get_All_InventoryItems_Returns_OK()
    {
        var token = CancellationToken.None;
        _inventoryServiceMock.Setup(s =>
                s.GetInventoryItems(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(InventoryDtos));

        var actionResult = await _controller.GetAllInventoryItemsAsync(token);

        actionResult.IsOk().AndShouldBe(InventoryDtos);
        _inventoryServiceMock.Verify(s => s.GetInventoryItems(token), Times.Once);
    }

    [Fact]
    private async Task Get_InventoryItem_With_Id_Returns_OK()
    {
        var token = CancellationToken.None;
        var itemId = DefaultItem.Id;
        _inventoryServiceMock.Setup(s =>
                s.GetInventoryItem(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(DefaultItemDto));

        var actionResult = await _controller.GetInventoryItemWithIdAsync(itemId, token);

        actionResult.IsOk().AndShouldBe(DefaultItemDto);
        _inventoryServiceMock.Verify(s => s.GetInventoryItem(itemId, token), Times.Once);
    }

    [Fact]
    private async Task Get_InventoryItem_With_Id_Returns_NotFound_When_Item_Does_Not_Exist()
    {
        var token = CancellationToken.None;
        var itemId = Guid.NewGuid();
        _inventoryServiceMock.Setup(s =>
                s.GetInventoryItem(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.FailedToFindItem<InventoryItemDto>());

        var actionResult = await _controller.GetInventoryItemWithIdAsync(itemId, token);

        actionResult.IsNotFound();
        _inventoryServiceMock.Verify(s => s.GetInventoryItem(itemId, token), Times.Once);
    }

    [Fact]
    private async Task Create_InventoryItem_Returns_Created()
    {
        var token = CancellationToken.None;
        _inventoryServiceMock.Setup(s =>
                s.CreateInventoryItem(It.IsAny<CreateInventoryItemDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(DefaultItemDto));
        _uriProviderMock.Setup(u => u.GetRequestUriWithId(It.IsAny<HttpRequest>(), It.IsAny<Guid>()))
            .Returns(new Uri("http://localhost/api/v1/inventory/1234"));

        var actionResult = await _controller.CreateInventoryItemAsync(_createInventoryItemDto, token);

        actionResult.IsCreated().AndShouldBe(DefaultItemDto);
        _inventoryServiceMock.Verify(s => s.CreateInventoryItem(_createInventoryItemDto, token), Times.Once);
    }

    [Fact]
    private async Task Create_InventoryItem_Returns_BadRequest_When_Input_Is_Invalid()
    {
        var token = CancellationToken.None;
        _inventoryServiceMock.Setup(s =>
                s.CreateInventoryItem(It.IsAny<CreateInventoryItemDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.FailedToValidate<CreateInventoryItemDto, InventoryItemDto>());

        var actionResult = await _controller.CreateInventoryItemAsync(_createInventoryItemDto, token);

        actionResult.IsBadRequest();
        _inventoryServiceMock.Verify(s => s.CreateInventoryItem(_createInventoryItemDto, token), Times.Once);
    }

    [Fact]
    private async Task Update_InventoryItem_Returns_OK()
    {
        var token = CancellationToken.None;
        var itemId = DefaultItem.Id;

        _inventoryServiceMock.Setup(s =>
                s.UpdateInventoryItem(It.IsAny<Guid>(), It.IsAny<UpdateInventoryItemDto>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(DefaultItemDto));

        var actionResult = await _controller.UpdateInventoryItemAsync(itemId, _updateDto, token);

        actionResult.IsOk().AndShouldBe(DefaultItemDto);
        _inventoryServiceMock.Verify(s => s.UpdateInventoryItem(itemId, _updateDto, token), Times.Once);
    }

    [Fact]
    private async Task Update_InventoryItem_Returns_BadRequest_When_Input_Is_Invalid()
    {
        var token = CancellationToken.None;
        var itemId = DefaultItem.Id;
        _inventoryServiceMock.Setup(s =>
                s.UpdateInventoryItem(It.IsAny<Guid>(), It.IsAny<UpdateInventoryItemDto>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.FailedToValidate<UpdateInventoryItemDto, InventoryItemDto>());

        var actionResult = await _controller.UpdateInventoryItemAsync(itemId, _updateDto, token);

        actionResult.IsBadRequest();
        _inventoryServiceMock.Verify(s => s.UpdateInventoryItem(itemId, _updateDto, token), Times.Once);
    }

    [Fact]
    private async Task Delete_InventoryItem_Returns_NoContent()
    {
        var token = CancellationToken.None;
        var itemId = DefaultItem.Id;
        _inventoryServiceMock.Setup(s =>
                s.DeleteInventoryItem(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(DefaultItemDto));

        var actionResult = await _controller.DeleteInventoryItemAsync(itemId, token);

        actionResult.IsNoContent();
        _inventoryServiceMock.Verify(s => s.DeleteInventoryItem(itemId, token), Times.Once);
    }

    [Fact]
    private async Task Delete_InventoryItem_Returns_BadRequest_When_Item_Does_Not_Exist()
    {
        var token = CancellationToken.None;
        var itemId = Guid.NewGuid();
        _inventoryServiceMock.Setup(s =>
                s.DeleteInventoryItem(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.FailedToFindItem<InventoryItemDto>());

        var actionResult = await _controller.DeleteInventoryItemAsync(itemId, token);
        actionResult.IsBadRequest();
        _inventoryServiceMock.Verify(s => s.DeleteInventoryItem(itemId, token), Times.Once);
    }
}
