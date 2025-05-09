using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.Models.Common;

namespace InventryOrderManagementSystem.BLL.SeviceInterfaces
{
    public interface IAuthServices
    {
        Task<Response<object>> LoginAsync(LoginDto loginDto);
        Task<Response<User>> RegisterAsync(UserDto user);
    }
}
