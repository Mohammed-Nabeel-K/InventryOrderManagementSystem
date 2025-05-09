using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.DAL.Models.Common;

namespace InventryOrderManagementSystem.BLL.SeviceInterfaces
{
    public interface IProductServices
    {
        Task<Response<List<ProductGetDto>>> GetAllProductsAsync();
    }
}
