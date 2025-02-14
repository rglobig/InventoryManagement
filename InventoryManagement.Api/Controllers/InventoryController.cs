using Asp.Versioning;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InventoryController(IInventoryService inventoryService) : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<InventoryItemDto>))]
        public async Task<ActionResult<ICollection<InventoryItemDto>>> GetAllInventoryItemsAsync(CancellationToken cancellationToken)
        {
            var items = await inventoryService.GetInventoryItems(cancellationToken);
            var data = items.Select(InventoryItemDto.From);
            return Ok(data);
        }

        [HttpGet("{id:guid}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventoryItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult<InventoryItemDto?>> GetInventoryItemWithIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var item = await inventoryService.GetInventoryItem(id, cancellationToken);

            if (item == null) return NotFound();

            var data = InventoryItemDto.From(item);

            return Ok(data);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InventoryItemDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult<InventoryItemDto>> CreateInventoryItemAsync([FromBody] CreateInventoryItemDto data, CancellationToken cancellationToken)
        {
            if(data is null) return BadRequest();

            var item = await inventoryService.CreateInventoryItem(data, cancellationToken);
            var dto = InventoryItemDto.From(item);

            return Created(nameof(GetInventoryItemWithIdAsync), dto);
        }

        [HttpPatch("{id:guid}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventoryItemDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult<InventoryItemDto?>> UpdateInventoryItemAsync([FromRoute] Guid id, [FromBody] UpdateInventoryItemDto data, CancellationToken cancellationToken)
        {
            var item = await inventoryService.UpdateInventoryItem(id, data, cancellationToken);

            if (item == null) return BadRequest();

            var dto = InventoryItemDto.From(item);
            return Ok(dto);
        }

        [HttpDelete("{id:guid}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> DeleteInventoryItemAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var deleted = await inventoryService.DeleteInventoryItem(id, cancellationToken);
            return deleted != null ? NoContent() : BadRequest();
        }
    }
}
