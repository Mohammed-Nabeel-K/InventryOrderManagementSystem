using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.DAL.RepositoryInterfaces
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmailAsync(string Email);
        Task<bool> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid id);
    }
}
