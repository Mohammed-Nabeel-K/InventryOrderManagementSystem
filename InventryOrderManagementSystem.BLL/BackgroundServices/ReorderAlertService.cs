using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InventryOrderManagementSystem.BLL.BackgroundServices
{
    public class ReorderAlertService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRunTime = DateTime.Today.AddHours(8); // 8:00 AM today

                Console.WriteLine($"Current Time: {now}");
                Console.WriteLine($"Next Run Time: {nextRunTime}");

                if (now > nextRunTime)
                    nextRunTime = nextRunTime.AddDays(1); // Schedule for next day if already past 8 AM

                var delay = nextRunTime - now;
                Console.WriteLine($"Delay {delay}");
                await Task.Delay(delay, stoppingToken);

                using (var scope = serviceProvider.CreateScope())
                {
                    Console.WriteLine($"Running Reorder Alert Service at: {DateTime.Now}");
                    var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                    //var adminService = scope.ServiceProvider.GetRequiredService<IAdminAlertService>(); // Assume you have this service
                    
                    var lowStockProducts = await productRepository.GetLowStockProducts();

                    //if (lowStockProducts.Any())
                    //{
                    //    await adminService.SendLowStockAlertAsync(lowStockProducts);
                    //}
                }
            }
        }
    }
}
