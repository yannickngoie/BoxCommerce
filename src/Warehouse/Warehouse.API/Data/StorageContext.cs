using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.API.Models;

namespace Warehouse.API.Data
{
    public class StorageContext: DbContext
    {
        public StorageContext(DbContextOptions<StorageContext> options)
: base(options)
        {
        }

        public DbSet<Storage> Storages { get; set; }
    }
}
