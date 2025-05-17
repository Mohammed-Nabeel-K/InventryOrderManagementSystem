using AutoMapper;
using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using InventryOrderManagementSystem.DAL.Models.Common;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using System.Net;

namespace InventryOrderManagementSystem.BLL.Services
{
    public class ReportsServices(IReportsRepository reportsRepository,IMapper mapper,IProductRepository productRepository) : IReportsServices
    {
        public async Task<Response<List<SaleData>>> SalesReportsBetweenDays(DateTime From,DateTime To)
        {
            var sales = await reportsRepository.GetSalesReportByDate(From, To);
            if (sales == null || sales.Count == 0)
            {
                return new Response<List<SaleData>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "No Sales Report Found"
                };
            }
            return new Response<List<SaleData>> 
            { 
                StatusCode = HttpStatusCode.OK,
                Message = "Sales Report Retrieved Successfully",
                Data = mapper.Map<List<SaleData>>(sales) 
            };
        }
        public async Task<Response<UserSalesData>> UserSalesAndTopProducts(Guid UserId, DateOnly Month)
        {
            var usersales = await reportsRepository.GetSalesByUserId(UserId, Month);
            return new Response<UserSalesData>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Top Products Retrieved Successfully",
                Data = usersales
            };
        }
        public async Task<Response<List<InventryDto>>> InventryWithStatus()
        {
            var products = await productRepository.GetAllAsync();
            if (products == null || products.Count == 0)
            {
                return new Response<List<InventryDto>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "No Products Found"
                };
            }
            var inventry = mapper.Map<List<InventryDto>>(products);
            return new Response<List<InventryDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Inventry Retrieved Successfully",
                Data = inventry
            };
        }
    }
}
