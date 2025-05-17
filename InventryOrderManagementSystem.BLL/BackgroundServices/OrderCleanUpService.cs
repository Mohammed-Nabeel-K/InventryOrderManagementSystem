using InventryOrderManagementSystem.DAL.Data;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InventryOrderManagementSystem.BLL.BackgroundServices
{
    public class OrderCleanUpService(IServiceProvider serviceProvider,ILogger<OrderCleanUpService> logger) : BackgroundService
    {
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    logger.LogInformation("Running Order Cleanup Service at: {Time}", DateTime.Now);    
                    var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                    var cutoffDate = DateTime.Now.AddDays(-3);

                    await orderRepository.GetOrdersNotCompletedInThreeDAys(cutoffDate);

                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
