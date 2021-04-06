using Common.Logging;
using Microsoft.EntityFrameworkCore;
using Production.API.Data;
using Production.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Production.API.Repositories.Interfaces
{
    public class ProductionRepository : IProductionRepository
    {
        protected readonly ProductionContext _dbContext;
        public ProductionRepository(ProductionContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }



        public async Task<IReadOnlyList<Activity>> GetItem(Expression<Func<Activity, bool>> predicate)
        {
            return await _dbContext.Set<Activity>().Where(predicate).ToListAsync();
        }

        public async Task <Activity> UpdateWorkItem(Activity item)
        {
            _dbContext.Activities.Update(item);
            await _dbContext.SaveChangesAsync();
            var result = item;

            return result;
        }

        public async Task<Activity> AddWorkItem(Activity item)
        {
            _dbContext.Activities.Add(item);
            await _dbContext.SaveChangesAsync();
            var result = item;

            return result;
        }   


        public Task<string> DeleteWorkItem(string OrderNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Activity>> GetWorkItems()
        {
            return await _dbContext.Activities.ToListAsync();
        }

        public async Task<Activity> CompleteWorkItem(Activity item)
        {
            Expression<Func<Activity, bool>> findWorkItem = o => o.OrderNumber == item.OrderNumber;
            var result = await GetItem(findWorkItem);
            var itemToComplete = new Activity();

            if (result.Any())
            {
                itemToComplete = result.FirstOrDefault();

                itemToComplete.OrderStatus = Status.Completed.ToString();
                _dbContext.Entry(itemToComplete).State = EntityState.Modified;
               // _dbContext.Activities.Update(itemToComplete);
                await _dbContext.SaveChangesAsync();
            }

            return itemToComplete;
        }

        public Task DeleteItem(string item)
        {
            throw new NotImplementedException();
        }
    }
}
