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
        public ActionResult<ICollection<InventoryItem>> GetAllInventoryItems()
        {
            var items = inventoryService.GetInventoryItems();
            return Ok(items);
        }
    }
}
