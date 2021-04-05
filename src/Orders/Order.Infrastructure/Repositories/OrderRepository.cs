using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Persistence;
using Order.Infrastructure.Persistence;
using Orders.Application.Contracts.Persistence;
using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Orders.Domain.Models.CustomerOrder>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<OrderLines> AddAsyncOrderLine(OrderLines entity)
        {
            if (entity != null)
            {
                _dbContext.OrderLines.Add(entity);
                await _dbContext.SaveChangesAsync();
            }

            return entity;
        }

        public Task DeleteAsyncOrderLine(OrderLines id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderLines> GetAsyncOrderLine(OrderLines id)
        {
            throw new NotImplementedException();
        }


        public Task DeleteAsyncOrderLine(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerOrder>> GetOrdersByRef(string orderRef)
        {
            var orderList = await _dbContext.Orders
                                .Where(o => o.OrderNumber== orderRef)
                                .ToListAsync();
            return orderList;
        }

        public async Task<IEnumerable<CustomerOrder>> GetAllOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }


        public  async Task<CustomerOrder> GetOrderStatus(string orderNumber)
        {
            var orderList = await _dbContext.Orders
                                   .Where(o => o.OrderNumber == orderNumber).FirstAsync();

            return orderList;

        }

        public async Task<Component> AddAsyncComponent(Component entity)
        {
            if (entity != null)
            {
                _dbContext.Components.Add(entity);
                await _dbContext.SaveChangesAsync();
            }

            return entity;
        }
    }
}