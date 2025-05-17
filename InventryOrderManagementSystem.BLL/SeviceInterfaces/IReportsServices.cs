using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.DAL.Models.Common;

namespace InventryOrderManagementSystem.BLL.SeviceInterfaces
{
    public interface IReportsServices
    {
        Task<Response<List<SaleData>>> SalesReportsBetweenDays(DateTime From, DateTime To);
        Task<Response<List<InventryDto>>> InventryWithStatus();
        Task<Response<UserSalesData>> UserSalesAndTopProducts(Guid UserId, DateOnly Month);
    }
}
