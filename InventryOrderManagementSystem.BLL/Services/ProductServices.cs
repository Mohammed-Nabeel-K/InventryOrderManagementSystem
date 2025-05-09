using AutoMapper;
using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
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
                    StatusCode = HttpStatusCode.NotFound,
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
    }
}
