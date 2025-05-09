using InventryOrderManagementSystem.DAL.Data;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InventryOrderManagementSystem.DAL.Repositories
{
    public class AuthRepository(AppDbContext dbContext) : IAuthRepository
    {
        public async Task<bool> CreateUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            return await dbContext.SaveChangesAsync() > 0 ;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }
    }
}
