using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventryOrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductServices productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await productService.GetAllProductsAsync();
            return StatusCode((int)result.StatusCode,result);
        }
    }
}
