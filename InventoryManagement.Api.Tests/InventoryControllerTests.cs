using FluentAssertions;
using InventoryManagement.Api.Controllers;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryManagement.Api.Tests;

public class InventoryControllerTests
{
    // [Fact]
    // async Task GetAllInventoryItems()
    // {
    //     List<InventoryItem> inventory = [new InventoryItem("iPhone", "Smartphone", 1, 1000)];
    //     List<InventoryItemDto> returnData = inventory.Select(InventoryItemDto.From).ToList();
    //
    //     var mock = new Mock<IInventoryService>();
    //     var token = new CancellationToken();
    //     mock.Setup(s => s.GetInventoryItems(token)).ReturnsAsync(inventory);
    //     var controller = new InventoryController(mock.Object);
    //
    //     var actionResult = await controller.GetAllInventoryItemsAsync(token);
    //
    //     var result = actionResult.Result as OkObjectResult;
    //     result.Should().NotBeNull();
    //     result!.Value.Should().BeEquivalentTo(returnData);
    //     mock.Verify(s => s.GetInventoryItems(token), Times.Once);
    // }
    //
    // [Fact]
    // async Task GetInventoryItemById_WithValidId_ResultsInOkWithObject()
    // {
    //     var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
    //     var returnData = InventoryItemDto.From(item);
    //     var id = item.Id;
    //
    //     var mock = new Mock<IInventoryService>();
    //     var token = new CancellationToken();
    //     mock.Setup(s => s.GetInventoryItem(id, token)).ReturnsAsync(item);
    //     var controller = new InventoryController(mock.Object);
    //
    //     var actionResult = await controller.GetInventoryItemWithIdAsync(id, token);
    //
    //     var result = actionResult.Result as OkObjectResult;
    //     result.Should().NotBeNull();
    //     result!.Value.Should().BeEquivalentTo(returnData);
    //     mock.Verify(s => s.GetInventoryItem(id, token), Times.Once);
    // }
    //
    // [Fact]
    // async Task GetInventoryItem_WithWrongId_ResultsInNonFound()
    // {
    //     var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
    //     var returnData = InventoryItemDto.From(item);
    //     var id = Guid.NewGuid();
    //
    //     var mock = new Mock<IInventoryService>();
    //     var token = new CancellationToken();
    //     mock.Setup(s => s.GetInventoryItem(item.Id, token)).ReturnsAsync(item);
    //     var controller = new InventoryController(mock.Object);
    //
    //     var actionResult = await controller.GetInventoryItemWithIdAsync(id, token);
    //
    //     actionResult.Result.Should().BeAssignableTo<NotFoundResult>();
    //     mock.Verify(s => s.GetInventoryItem(id, token), Times.Once);
    // }
    //
    // [Fact]
    // async Task CreateInventoryItem_ResultsInCreated()
    // {
    //     var input = new CreateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
    //     var item = input.ToInventoryItem();
    //     var returnData = InventoryItemDto.From(item);
    //
    //     var mock = new Mock<IInventoryService>();
    //     var token = new CancellationToken();
    //     mock.Setup(s => s.CreateInventoryItem(input, token)).ReturnsAsync(item);
    //     var controller = new InventoryController(mock.Object);
    //
    //     var actionResult = await controller.CreateInventoryItemAsync(input, token);
    //
    //     var result = actionResult.Result as CreatedResult;
    //     result.Should().NotBeNull();
    //     result!.Value.Should().BeEquivalentTo(returnData);
    //     mock.Verify(s => s.CreateInventoryItem(input, token), Times.Once);
    // }
    //
    // [Fact]
    // async Task UpdateInventoryItem_ResultsInOk()
    // {
    //     var input = new UpdateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
    //     var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
    //     var returnData = InventoryItemDto.From(item);
    //     var id = item.Id;
    //
    //     var mock = new Mock<IInventoryService>();
    //     var token = new CancellationToken();
    //     mock.Setup(s => s.UpdateInventoryItem(id, input, token)).ReturnsAsync(item);
    //     var controller = new InventoryController(mock.Object);
    //
    //     var actionResult = await controller.UpdateInventoryItemAsync(id, input, token);
    //
    //     var result = actionResult.Result as OkObjectResult;
    //     result.Should().NotBeNull();
    //     result!.Value.Should().BeEquivalentTo(returnData);
    //     mock.Verify(s => s.UpdateInventoryItem(id, input, token), Times.Once);
    // }
    //
    // [Fact]
    // async Task UpdateInventoryItem_ResultsInBadRequest()
    // {
    //     var input = new UpdateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
    //     InventoryItem item = null!;
    //     var id = Guid.NewGuid();
    //
    //     var mock = new Mock<IInventoryService>();
    //     var token = new CancellationToken();
    //     mock.Setup(s => s.UpdateInventoryItem(id, input, token)).ReturnsAsync(item);
    //     var controller = new InventoryController(mock.Object);
    //
    //     var actionResult = await controller.UpdateInventoryItemAsync(id, input, token);
    //
    //     actionResult.Result.Should().BeAssignableTo<BadRequestResult>();
    //     mock.Verify(s => s.UpdateInventoryItem(id, input, token), Times.Once);
    // }
    //
    // [Fact]
    // async Task DeleteInventoryItem_ResultsInNoContent()
    // {
    //     var input = new InventoryItem("iPhone", "Smartphone", 1, 1000);
    //     var id = input.Id;
    //
    //     var mock = new Mock<IInventoryService>();
    //     var token = new CancellationToken();
    //     mock.Setup(s => s.DeleteInventoryItem(id, token)).ReturnsAsync(input);
    //     var controller = new InventoryController(mock.Object);
    //
    //     var actionResult = await controller.DeleteInventoryItemAsync(id, token);
    //     actionResult.Should().BeAssignableTo<NoContentResult>();
    // }
    //
    // [Fact]
    // async Task DeleteInventoryItem_ResultsInBadRequest()
    // {
    //     InventoryItem input = null!;
    //     var id = Guid.NewGuid();
    //
    //     var mock = new Mock<IInventoryService>();
    //     var token = new CancellationToken();
    //     mock.Setup(s => s.DeleteInventoryItem(id, token)).ReturnsAsync(input);
    //     var controller = new InventoryController(mock.Object);
    //
    //     var actionResult = await controller.DeleteInventoryItemAsync(id, token);
    //     actionResult.Should().BeAssignableTo<BadRequestResult>();
    //     mock.Verify(s => s.DeleteInventoryItem(id, token), Times.Once);
    // }
}
