using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.API.Data;
using Warehouse.API.Models;

namespace Warehouse.API.Repositories
{
    public class StorageRepository: IStorageRepository
    {

        private readonly StorageContext _dbContext;
        public StorageRepository(StorageContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public async Task DeleteStorage(int id)
        {

            var itemToRemove = await _dbContext.Storages.FindAsync(id);

            if (itemToRemove != null)
            {

                var storage = new Storage(id);
                _dbContext.Entry(storage).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Storage> InsertStorage(Storage storage)
        {
            _dbContext.Storages.Add(storage);
            await _dbContext.SaveChangesAsync();
            var result = storage;

            return result;
        }
    }
}
