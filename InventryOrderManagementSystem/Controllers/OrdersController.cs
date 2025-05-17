using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventryOrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderServices orderServices,ILogger<OrdersController> logger) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDto orderDto)
        {
            logger.LogInformation("Creating order");
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var result = await orderServices.AddOrderAsync(orderDto,userId);
            return StatusCode((int)result.StatusCode, result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(Guid id)
        {
            logger.LogInformation($"Getting order with id {id}");
            var result = await orderServices.GetOrderByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
