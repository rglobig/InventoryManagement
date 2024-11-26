using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController(IInventoryService inventoryService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ICollection<InventoryItemDto>> GetAllInventoryItems()
        {
            var items = inventoryService.GetInventoryItems();
            var data = items.Select(InventoryItemDto.From);
            return Ok(data);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<InventoryItemDto?> GetInventoryItemWithId(Guid id)
        {
            var item = inventoryService.GetInventoryItem(id);

            if (item == null) return NotFound();

            var data = InventoryItemDto.From(item);

            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<InventoryItemDto> CreateInventoryItem(CreateInventoryItemDto data)
        {
            var item = inventoryService.CreateInventoryItem(data);
            var dto = InventoryItemDto.From(item);
            return Created(nameof(CreateInventoryItem), dto);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<InventoryItemDto?> UpdateInventoryItem(Guid id, UpdateInventoryItemDto data)
        {
            if (!inventoryService.TryUpdateInventoryItem(id, data, out InventoryItem? item))
            {
                return BadRequest();
            }
            var dto = InventoryItemDto.From(item!);
            return Ok(dto);
        }
    }
}
