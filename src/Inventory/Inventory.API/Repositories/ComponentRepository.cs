using Inventory.API.Data;
using Inventory.API.Models;
using Inventory.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        protected readonly InventoryContext _dbContext;
        public ComponentRepository(InventoryContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task DeleteComponent(Guid ID)
        {

            var itemToRemove = await _dbContext.Components.FindAsync(ID);

            if (itemToRemove != null)
            {
                var component = new Component(ID);

                _dbContext.Entry(component).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<Component> GetComponentById(Guid id, bool inStock = false)
        {
            var component = new Component();

            if (inStock)
            {
                component = await _dbContext.Components.Where(x => x.ID == id && x.InStock == inStock).FirstOrDefaultAsync();
            }
            else
            {
                component = await _dbContext.Components.FindAsync(id);
            }

            return component;
        }

        public async Task<IEnumerable<Component>> GetComponents()
        {
            return await _dbContext.Components.ToListAsync();
        }

        public async Task<Component> InsertComponent(Component component)
        {
            _dbContext.Components.Add(component);
            await _dbContext.SaveChangesAsync();
            var result = component;

            return result;
        }

        public async Task<Component> UpdateComponent(Component component)
        {
            _dbContext.Components.Update(component);
            await _dbContext.SaveChangesAsync();
            var result = component;

            return result;
        }
    }
}
