﻿using Production.API.Data;
using Production.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
 

        public async Task <Activity> UpdateWorkItem(Activity item)
        {
            _dbContext.Products.Update(item);
            await _dbContext.SaveChangesAsync();
            var result = item;

            return result;
        }

        public async Task<Activity> AddWorkItem(Activity item)
        {
            _dbContext.Products.Add(item);
            await _dbContext.SaveChangesAsync();
            var result = item;

            return result;
        }

        public Task<List<Activity>> GetItems(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteWorkItem(string OrderNumber)
        {
            throw new NotImplementedException();
        }
    }
}