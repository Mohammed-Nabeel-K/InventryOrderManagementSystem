using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.DAL.Models.Common;

namespace InventryOrderManagementSystem.BLL.SeviceInterfaces
{
    public interface IProductServices
    {
        Task<Response<List<ProductGetDto>>> GetAllProductsAsync();
        Task<Response<object>> UpdateStockAsync(StockMovementDto stockMovementDto, Guid UserId);
        Task<Response<ProductGetDto>> CreateProductAsync(ProductDto productDto, Guid UserId);
        Task<Response<ProductGetDto>> UpdateProductAsync(Guid productId, ProductDto productDto, Guid UserId);
    }
}
