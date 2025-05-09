using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.BLL.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoles Roles { get; set; }

    }
}
