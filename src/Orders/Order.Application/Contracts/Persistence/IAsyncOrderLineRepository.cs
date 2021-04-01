using Orders.Domain.Models;
using System.Threading.Tasks;

namespace Order.Application.Contracts.Persistence
{
   public interface IAsyncOrderLineRepository<T> where T: OrderLines
    {
        Task <T>AddAsyncOrderLine (T lines);
        Task <T> GetAsyncOrderLine(T id);
        Task DeleteAsyncOrderLine(T id);
    }
}
