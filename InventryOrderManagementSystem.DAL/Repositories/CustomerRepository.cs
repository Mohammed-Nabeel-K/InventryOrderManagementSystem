using InventryOrderManagementSystem.DAL.Data;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InventryOrderManagementSystem.DAL.Repositories
{
    public class CustomerRepository(AppDbContext dbContext) : ICustomerRepository
    {
        public async Task<bool> IsCustomerExists(Guid CustomerId)
        {
            return await dbContext.Customers.AnyAsync(c => c.Id == CustomerId);
        }
    }
}
