using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventryOrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderServices orderServices) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDto orderDto)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            var result = await orderServices.AddOrderAsync(orderDto,userId);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(Guid id)
        {
            var result = await orderServices.GetOrderByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
