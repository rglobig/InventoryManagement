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
    [Fact]
    public void GetAllInventoryItems()
    {
        List<InventoryItem> inventory = [new InventoryItem("iPhone", "Smartphone", 1, 1000)];
        List<InventoryItemDto> returnData = inventory.Select(InventoryItemDto.From).ToList();

        var mock = new Mock<IInventoryService>();
        mock.Setup(s => s.GetInventoryItems()).Returns(inventory);
        var controller = new InventoryController(mock.Object);

        var actionResult = controller.GetAllInventoryItems();

        var result = actionResult.Result as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().BeEquivalentTo(returnData);
    }

    [Fact]
    public void GetInventoryItemById_WithValidId_ResultsInOkWithObject()
    {
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var returnData = InventoryItemDto.From(item);
        var id = item.Id;

        var mock = new Mock<IInventoryService>();
        mock.Setup(s => s.GetInventoryItem(id)).Returns(item);
        var controller = new InventoryController(mock.Object);

        var actionResult = controller.GetInventoryItemWithId(id);

        var result = actionResult.Result as OkObjectResult;
        result.Should().NotBeNull();

        result!.Value.Should().BeEquivalentTo(returnData);
    }

    [Fact]
    public void GetInventoryItem_WithWrongId_ResultsInNonFound()
    {
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var returnData = InventoryItemDto.From(item);
        var id = Guid.NewGuid();

        var mock = new Mock<IInventoryService>();
        mock.Setup(s => s.GetInventoryItem(item.Id)).Returns(item);
        var controller = new InventoryController(mock.Object);

        var actionResult = controller.GetInventoryItemWithId(id);

        var result = actionResult.Result as NotFoundResult;
        result.Should().NotBeNull();
    }

    [Fact]
    public void CreateInventoryItem_ResultsInCreated()
    {
        var input = new CreateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        var item = input.ToInventoryItem();
        var returnData = InventoryItemDto.From(item);

        var mock = new Mock<IInventoryService>();
        mock.Setup(s => s.CreateInventoryItem(input)).Returns(item);
        var controller = new InventoryController(mock.Object);

        var actionResult = controller.CreateItem(input);

        var result = actionResult.Result as CreatedResult;
        result.Should().NotBeNull();
        result!.Value.Should().BeEquivalentTo(returnData);
    }
}
