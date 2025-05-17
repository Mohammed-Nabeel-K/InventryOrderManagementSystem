using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventryOrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController(IReportsServices reportsServices,ILogger<ReportsController> logger) : ControllerBase
    {
        [HttpGet("sales")]
        public async Task<IActionResult> GetReports(DateTime From, DateTime To)
        {
            logger.LogInformation("Fetching sales reports between {From} and {To}", From, To);
            var result = await reportsServices.SalesReportsBetweenDays(From, To);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("inventory-status")]
        public async Task<IActionResult> GetInventoryStatus()
        {
            logger.LogInformation("Fetching inventory status");
            var result = await reportsServices.InventryWithStatus();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("user-sales")]
        public async Task<IActionResult> GetUserSales(Guid UserId, DateOnly Month)
        {
            logger.LogInformation("Fetching user sales for user {UserId} in month {Month}", UserId, Month);
            var result = await reportsServices.UserSalesAndTopProducts(UserId, Month);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
