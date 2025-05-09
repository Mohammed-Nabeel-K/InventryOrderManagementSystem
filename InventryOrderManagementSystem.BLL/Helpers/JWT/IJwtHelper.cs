using InventryOrderManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventryOrderManagementSystem.BLL.Helpers.JWT
{
    public interface IJwtHelper
    {
        string GenerateToken(User user);
    }
}
