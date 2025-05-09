using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventryOrderManagementSystem.DAL.RepositoryInterfaces
{
    public interface ICustomerRepository
    {
        Task<bool> IsCustomerExists(Guid CustomerId);
    }
}
