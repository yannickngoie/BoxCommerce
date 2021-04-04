using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Contracts.Persistence;
using Orders.Domain.Models;
namespace Orders.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<CustomerOrder>, IAsyncOrderLineRepository<OrderLines>, IAsyncComponentRepository<Component>
    {
        Task<IEnumerable<CustomerOrder>> GetOrdersByRef(string IDNumber);

       
    }
}
