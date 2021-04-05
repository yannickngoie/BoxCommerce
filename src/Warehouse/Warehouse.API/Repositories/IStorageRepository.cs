using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.API.Repositories
{
    interface IStorageRepository
    {
     Task DeleteStorage(int id);
     Task<Storage> InsertStorage(Storage storage);
    }
}
