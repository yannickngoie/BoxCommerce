using System;
using System.Collections.Generic;
using Orders.Domain.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Contracts.Persistence
{
    public interface IAsyncComponentRepository<T> where T: Component
    {
        Task <T> AddAsyncComponent(T lines);
       
    }
}
