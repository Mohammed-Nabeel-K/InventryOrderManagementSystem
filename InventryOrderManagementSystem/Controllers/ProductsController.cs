using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.Services;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace InventryOrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(
        IProductServices productService,
        IMemoryCache memoryCache,
        ILogger<ProductsController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            logger.LogInformation("Fetching all products from cache or service.");
            const string cacheKey = "product_list";

            if (!memoryCache.TryGetValue(cacheKey, out Response<List<ProductDto>> cachedResult))
            {
                logger.LogInformation("Cache miss. Fetching products from service.");
                var result = await productService.GetAllProductsAsync();

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.UtcNow.AddMinutes(5)
                    };
                    memoryCache.Set(cacheKey, result, cacheOptions);
                }

                return StatusCode((int)result.StatusCode, result);
            }

            return StatusCode((int)cachedResult.StatusCode, cachedResult);
        }
        [HttpPost("add-stock")]
        public async Task<IActionResult> UpdateStockAsync(StockMovementDto stockMovementDto)
        {
            logger.LogInformation("Updating stock for product with ID: {ProductId}", stockMovementDto.ProductId);
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var result = await productService.UpdateStockAsync(stockMovementDto, userId);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = nameof(UserRoles.Admin) + "," + nameof(UserRoles.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(ProductDto productDto)
        {
            logger.LogInformation("Creating new product with name: {ProductName}", productDto.Name);
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var result = await productService.CreateProductAsync(productDto, userId);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = nameof(UserRoles.Admin))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, ProductDto productDto)
        {
            logger.LogInformation("Updating product with ID: {ProductId}", id);
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var result = await productService.UpdateProductAsync(id, productDto, userId);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
