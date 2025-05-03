using Asp.Versioning;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class InventoryController(IInventoryService inventoryService) : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<InventoryItemDto>))]
    public async Task<ActionResult<IReadOnlyList<InventoryItemDto>>> GetAllInventoryItemsAsync(
        CancellationToken cancellationToken)
    {
        var result = await inventoryService.GetInventoryItems(cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventoryItemDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventoryItemDto?>> GetInventoryItemWithIdAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await inventoryService.GetInventoryItem(id, cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InventoryItemDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventoryItemDto>> CreateInventoryItemAsync(
        [FromBody] CreateInventoryItemDto data, CancellationToken cancellationToken)
    {
        var result = await inventoryService.CreateInventoryItem(data, cancellationToken);

        if (!result.IsSuccess) return BadRequest(result.Error);

        var item = result.Value;
        var locationUri = Url.Action(string.Empty, "Inventory",
            new { id = item.Id, version = "1.0" });

        return Created(locationUri, item);
    }

    [HttpPatch("{id:guid}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventoryItemDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventoryItemDto?>> UpdateInventoryItemAsync([FromRoute] Guid id,
        [FromBody] UpdateInventoryItemDto data, CancellationToken cancellationToken)
    {
        var result = await inventoryService.UpdateInventoryItem(id, data, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<ActionResult> DeleteInventoryItemAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await inventoryService.DeleteInventoryItem(id, cancellationToken);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}
