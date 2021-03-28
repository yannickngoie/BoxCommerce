using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Domain.Models;
namespace Orders.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<CustomerOrder>
    {
        Task<IEnumerable<CustomerOrder>> GetOrdersByUserName(string userName);
    }
}
