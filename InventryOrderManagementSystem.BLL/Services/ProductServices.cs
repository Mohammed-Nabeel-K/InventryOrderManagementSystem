using AutoMapper;
using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.Models.Common;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using System.Net;

namespace InventryOrderManagementSystem.BLL.Services
{
    public class ProductServices(IMapper mapper, IProductRepository productRepository) : IProductServices
    {
        public async Task<Response<List<ProductGetDto>>> GetAllProductsAsync()
        {
            var products = await productRepository.GetAllAsync();
            if (products == null || !products.Any())
            {
                return new Response<List<ProductGetDto>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "No products found"
                };
            }
            var productDtos = mapper.Map<List<ProductGetDto>>(products);
            return new Response<List<ProductGetDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Products retrieved successfully",
                Data = productDtos
            };
        }
        public async Task<Response<object>> UpdateStockAsync(StockMovementDto stockMovementDto, Guid UserId)
        {
            var isExists = await productRepository.IsProductExists(stockMovementDto.ProductId);
            if (!isExists)
            {
                return new Response<object>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Product not found"
                };
            }
            var stockMovement = mapper.Map<StockMovement>(stockMovementDto);
            stockMovement.Timstamp = DateTime.UtcNow;
            stockMovement.PerformedBy = UserId;
            var result = await productRepository.UpdateStockAsync(stockMovement);
            return new Response<object>
            {
                StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.InternalServerError,
                Message = result ? "Stock added successfully" : "Failed to add stock",
                Data = result ? new object() : null
            };
        }
        public async Task<Response<ProductGetDto>> CreateProductAsync(ProductDto productDto, Guid UserId)
        {
            var product = mapper.Map<Product>(productDto);
            product.CreatedAt = DateTime.UtcNow;
            product.LastUpdatedAt = DateTime.UtcNow;
            product.UpdatedBy = UserId;
            product.CreatedBy = UserId;
            var createdProduct = await productRepository.CreateProductAsync(product);
            return new Response<ProductGetDto>
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Product created successfully",
                Data = mapper.Map<ProductGetDto>(createdProduct)
            };
        }
        public async Task<Response<ProductGetDto>> UpdateProductAsync(Guid productId, ProductDto productDto, Guid UserId)
        {
            var product = await productRepository.ProductById(productId);
            if (product == null)
            {
                return new Response<ProductGetDto>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Product not found"
                };
            }
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.QuantityInStock = productDto.QuantityInStock;
            product.LastUpdatedAt = DateTime.UtcNow;
            product.UpdatedBy = UserId;
            product.ReorderLevel = productDto.ReorderLevel;
            product.IsActive = productDto.IsActive;
            var isSuccess = await productRepository.UpdateProductAsync(product);
            return new Response<ProductGetDto>
            {
                StatusCode = isSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError,
                Message = isSuccess ? "Product updated successfully" : "Failed to update product",
                Data = isSuccess ? mapper.Map<ProductGetDto>(product) : null
            };
        }
    }
}
